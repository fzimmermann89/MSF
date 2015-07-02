using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;

namespace ClusterNum
{
    class NumVariator
    {
        public struct result
        {
            public double beta;
            public double[] ljapunow;
            public double[] rms;
            public result(double beta, double[] rms, double[] ljapunow)
            {
                this.beta = beta;
                this.rms = rms;
                this.ljapunow = ljapunow;
            }
        }




        public double betamin, betamax, sigma, delta, pertubation;
        public int betasteps, pre, rec, nodecount;
        private NumIterator iterator;
        private NumIterator pertiterator;
        double[][] adjmatrix;
        int[][] cluster;
        Action<result> callback;


        public NumVariator(double[][] adjMatrix, double betamin, double betamax, int betasteps, double sigma, double delta, double pertubation, int pre, int rec, int[][] cluster, Action<result> callback)
        {
            this.betamax = betamax;
            this.betamin = betamin;
            this.betasteps = betasteps;
            this.pre = pre;
            this.rec = rec;
            this.sigma = sigma;
            this.delta = delta;
            this.pertubation = pertubation;
            this.adjmatrix = adjMatrix;
            this.nodecount = adjMatrix.Length;
            this.callback = callback;
            this.cluster = cluster;
        }

        public void DoWork()
        {
            for (int ibeta = 0; ibeta <= betasteps; ibeta++)
            {
                double beta = betamin + ibeta * (betamax - betamin) / betasteps;

                //RMS
                pertiterator = new NumIterator(adjmatrix, beta, sigma, delta);
                pertiterator.pertubation = pertubation;
                double[] rms = new double[cluster.Length];
                pertiterator.iterate(pre);
                for (int itime = 0; itime < rec; itime++)
                {
                    pertiterator.iterate();
                    for (int icluster = 0; icluster < cluster.Length; icluster++)
                    {
                        rms[icluster] += MS(pertiterator.xt.Last())[icluster];
                    }
                }
                for (int i = 0; i < rms.Length; i++)
                {
                    rms[i] /= rec;
                    rms[i] = Math.Sqrt(rms[i]);
                }

                //Ljapunow
                double[] ljapunow = new double[cluster.Length];

                //ljapunator test

                double[][] TMat;
                double x = 1.0;
                double y = -0.5;
                double a = Math.Sqrt(2.0) / 2.0;
                double b = -a;
                double z = 0.5;

                string tmattext = @"0 0 0 0 b 0 0 0 0 b 0 
0 0 0 b 0 b 0 0 0 0 0 
0 0 0 0 0 0 0 0 0 0 x 
b 0 0 0 0 0 0 b 0 0 0 
0 y y 0 0 0 y 0 y 0 0 
0 0 0 b 0 a 0 0 0 0 0 
0 y z 0 0 0 y 0 z 0 0 
0 0 0 0 b 0 0 0 0 a 0 
b 0 0 0 0 0 0 a 0 0 0 
0 0 a 0 0 0 0 0 b 0 0 
0 b 0 0 0 0 a 0 0 0 0 ";

                tmattext = tmattext.Replace("x", x.ToString());
                tmattext = tmattext.Replace("y", y.ToString());
                tmattext = tmattext.Replace("a", a.ToString());
                tmattext = tmattext.Replace("b", b.ToString());
                tmattext = tmattext.Replace("z", z.ToString());

                TMat = Helper.MatrixFromString(tmattext);

                NumIterator smIterator = new NumIterator(adjmatrix, beta, sigma, delta);
                smIterator.iterate(500);

                List<double[]> smts = new List<double[]>();
                for (int i = 0; i < smIterator.xt.Count; i++)
                {
                    double[] add = new double[cluster.Length];
                    for (int j = 0; j < cluster.Length; j++)
                    {
                        int node0 = cluster[j][0];
                        add[j] = smIterator.xt[i][node0];
                    }
                    smts.Add(add);
                }

              
                for (int inum = 0; inum < nodecount; inum++)
                {
                    double pert = 0.5;
                    Ljapunator punator = new Ljapunator(adjmatrix, TMat, cluster, smts, beta, sigma, delta);

                    punator.etat[0][inum] = pert;

                    for (int i = 0; i < smIterator.xt.Count - 2; i++)
                    {
                        punator.iterate();
                    }
                }



                //ausgabe
                result ret_result = new result(beta, rms, ljapunow);
                callback(ret_result);

            }
        }
        private double[] MS(double[] xs)
        {
            double[] ms = new double[cluster.Length];
            for (int i = 0; i < cluster.Length; i++)
            {//über die cluster
                //mittelwert
                double mid = 0;
                for (int j = 0; j < cluster[i].Length; j++)
                {
                    int nodenum = cluster[i][j];
                    mid += xs[nodenum];
                }
                mid /= (double)cluster[i].Length;

                double tmprms = 0;
                for (int j = 0; j < cluster[i].Length; j++)
                {
                    int nodenum = cluster[i][j];
                    tmprms += (mid - xs[nodenum]) * (mid - xs[nodenum]);
                }
                tmprms = tmprms / cluster[i].Length;
                ms[i] += tmprms;

            }
            return ms;
        }

        private double varsqrsum(double[] a, double[] b)
        {
            double varsqrsum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                varsqrsum += (a[i] - b[i]) * (a[i] - b[i]);
            }
            return varsqrsum;
        }



        private double[] pertubate(double[] xi, double pert, int clusternum)
        {
            double[] pertxi = new double[xi.Length];
            Random rand = new Random();
            int[] clusternodes = cluster[clusternum];
            foreach (int nodenum in clusternodes)
            {
                //double err = (rand.NextDouble() - 0.5) * 2.0 * pertubation;
                double u1 = rand.NextDouble();
                double u2 = rand.NextDouble();
                double randnormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double err = pertubation * randnormal;


                pertxi[nodenum] = xi[nodenum] + err;
            }
            return pertxi;
        }
    }

}