using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;
using System.Windows.Forms;

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

       // private const double ljapunowPertubation = 0.01;
        public double betamin, betamax, sigma, delta, noise, pertubation;
        public int betasteps, pre, rec;

        double[,] adjmatrix;
        int[][] cluster;
        Action<result> callback;

        double[,] TMat;
        double[,] TMatInverse;
        double[,] BMat;
        double[][,] EMats;
        double[][,] JMats;
        int[][] clusterTransform;

        int nodeCount;

        Random rand = new Random();

        public NumVariator(double[,] adjMatrix, double betamin, double betamax, int betasteps, double sigma, double delta, double noise, double pert, int pre, int rec, int[][] cluster, Action<result> callback)
        {
            this.betamax = betamax;
            this.betamin = betamin;
            this.betasteps = betasteps;
            this.pre = pre;
            this.rec = rec;
            this.sigma = sigma;
            this.delta = delta;
            this.noise = noise;
            this.pertubation = pert;
            this.adjmatrix = adjMatrix;
            this.callback = callback;
            this.cluster = cluster;
            this.nodeCount = adjmatrix.GetLength(0);


            // Zeugs für BMat, JMats und clusterTransform berechnung, wird übergeben an Ljapunator
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

            TMatInverse = TMat.Inverse();
            EMats = new double[cluster.Length][,];
            JMats = new double[cluster.Length][,];
            for (int ci = 0; ci < cluster.Length; ci++)
            {
                EMats[ci] = new double[nodeCount, nodeCount];

            }
            for (int ci = 0; ci < cluster.Length; ci++)
            {
                for (int j = 0; j < cluster[ci].Length; j++)
                {
                    int nodenum = cluster[ci][j];
                    EMats[ci][nodenum, nodenum] = 1.0;
                }

                JMats[ci] = TMat.Multiply(EMats[ci]).Multiply(TMatInverse);
            }

            BMat = TMat.Multiply(adjmatrix).Multiply(TMatInverse);

            double[,] clusterTransformMatrix = new double[nodeCount, nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                for (int m = 0; m < cluster.Length; m++)
                {
                    if (JMats[m][i, i] != 0)
                    {
                        clusterTransformMatrix[i, i] = m;
                    }
                }
            }
            clusterTransform = new int[cluster.Length][];
            for (int m = 0; m < cluster.Length; m++)
            {
                clusterTransform[m] = new int[cluster[m].Length];
                int cnt = 0;
                for (int i = 0; i < nodeCount; i++)
                {
                    if (clusterTransformMatrix[i, i] == m)
                    {
                        clusterTransform[m][cnt] = i;
                        cnt++;
                    }
                }
            }
        }

        public void DoWork()
        {



            for (int ibeta = 0; ibeta <= betasteps; ibeta++)
            {
                double beta = betamin + ibeta * (betamax - betamin) / betasteps;

                //RMS
                NumIterator rmsIterator = new NumIterator(adjmatrix, beta, sigma, delta, pertubation);
                rmsIterator.noise = noise;
                double[] rms = new double[cluster.Length];
                rmsIterator.iterate(pre);
                for (int itime = 0; itime < rec; itime++)
                {
                    rmsIterator.iterate();
                    for (int icluster = 0; icluster < cluster.Length; icluster++)
                    {
                        rms[icluster] += MS(rmsIterator.xt.Last())[icluster];
                    }
                }
                for (int i = 0; i < rms.Length; i++)
                {
                    rms[i] /= rec;
                    rms[i] = Math.Sqrt(rms[i]);
                }

                //Ljapunow    
                double[] ljapunow = new double[cluster.Length];
                //synchrone orbits berechnen


                NumIterator smIterator = new NumIterator(adjmatrix, beta, sigma, delta, pertubation);
                smIterator.iterate(pre + rec);

                List<double[]> transDoneSynchManifolds = smIterator.xt.GetRange(pre, rec);

                List<double[]> smts = new List<double[]>();
                for (int i = 0; i < transDoneSynchManifolds.Count; i++)
                {
                    double[] add = new double[cluster.Length];
                    for (int j = 0; j < cluster.Length; j++)
                    {
                        int node0 = cluster[j][0];
                        add[j] = transDoneSynchManifolds[i][node0];
                    }
                    smts.Add(add);
                }

                ljapunow = ljapunow.Add(Double.MinValue);

                for (int m = 0; m < clusterTransform.Length; m++)
                {
                    for (int i = 0; i < clusterTransform[m].Length; i++)
                    {
                        int etanodenum = clusterTransform[m][i];
                        if (etanodenum >= clusterTransform.Length) // unterer Block
                        {
                            Ljapunator punator = new Ljapunator(JMats, BMat, cluster, smts, beta, sigma, delta);
                            punator.etat[0][etanodenum] = pertubation;
                            punator.iterate(rec);
                            ljapunow[m] = Math.Max(punator.ljapunowSum / (double)rec, ljapunow[m]);

                        }
                    }
                }

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

    }

}

