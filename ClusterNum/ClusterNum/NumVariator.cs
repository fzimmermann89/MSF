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
                pertiterator = new NumIterator(adjmatrix, beta, sigma, delta);
                pertiterator.pertubation = pertubation;
                NumIterator tmpiterator = new NumIterator(adjmatrix, beta, sigma, delta);

                double[] ljapunow = new double[cluster.Length];
                double[] rms = new double[cluster.Length];

                pertiterator.iterate(pre);

                for (int itime = 0; itime < rec; itime++)
                {
                    pertiterator.iterate();

                    //RMS
                    for (int icluster = 0; icluster < cluster.Length; icluster++)
                    {
                        rms[icluster] += MS(pertiterator.xt.Last())[icluster];

                    }

                }

                //rms berechnung
                for (int i = 0; i < rms.Length; i++)
                {
                    rms[i] /= rec;
                    rms[i] = Math.Sqrt(rms[i]);
                }


                for (int icluster = 0; icluster < cluster.Length; icluster++)
                {
                    if (cluster[icluster].Length > 1)
                    {
                        int maxrepeat = 1;

                        for (int irepeat = 0; irepeat < maxrepeat; irepeat++)
                        {
                            NumIterator ljapiterator = new NumIterator(adjmatrix, beta, sigma, delta);
                            NumIterator refiterator = new NumIterator(adjmatrix, beta, sigma, delta);
                            ljapiterator.iterate(pre);
                            refiterator.iterate(pre);

                            ljapiterator.xt[ljapiterator.xt.Count - 1] = pertubate(ljapiterator.xt.Last(), pertubation, icluster);
                            double referror = Math.Sqrt(varsqrsum(ljapiterator.xt.Last(), refiterator.xt.Last()));
                            double[] tmpljapunow = new double[cluster.Length];
                            for (int itime = 0; itime < rec; itime++)
                            {
                                double olderror = Math.Sqrt(varsqrsum(ljapiterator.xt.Last(), refiterator.xt.Last()));
                                //olderror ist immer referror
                                ljapiterator.iterate();
                                refiterator.iterate();
                                double newerror = Math.Sqrt(varsqrsum(ljapiterator.xt.Last(), refiterator.xt.Last()));
                                double summand = Math.Log(newerror / olderror);
                                tmpljapunow[icluster] += summand;
                                //normalisieren
                                double scale = newerror / referror;
                                for (int inode = 0; inode < nodecount; inode++)
                                {
                                    ljapiterator.xt.Last()[inode] = refiterator.xt.Last()[inode] + (ljapiterator.xt.Last()[inode] - refiterator.xt.Last()[inode]) / scale;
                                }

                            }
                            ljapunow[icluster] += tmpljapunow[icluster] / rec;
                        }
                        ljapunow[icluster] /= maxrepeat;
                    }

                }
                //ljap berechnung
                for (int i = 0; i < ljapunow.Length; i++)
                {

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