using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public int betasteps, pre, rec;
        private NumIterator iterator;
        int[][] adjmatrix;
        int[][] cluster;
        Action<result> callback;


        public NumVariator(int[][] adjMatrix, double betamin, double betamax, int betasteps, double sigma, double delta, double pertubation, int pre, int rec, int[][] cluster, Action<result> callback)
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
            this.callback = callback;
            this.cluster = cluster;
        }

        public void DoWork()
        {

            for (int ibeta = 0; ibeta <= betasteps; ibeta++)
            {

                double[] ljapunow = new double[cluster.Length];
                double[] rms = new double[cluster.Length];
                double beta = betamin + ibeta * (betamax - betamin) / betasteps;

                iterator = new NumIterator(adjmatrix, beta, sigma, delta);
                iterator.pertubation = pertubation;
               

                for (int itime = 0; itime < pre; itime++)
                {
                    iterator.iterate();
                }

                for (int itime = 0; itime < rec; itime++)
                {
                    iterator.iterate();

                    //rms berechnung
                    double[] xs = iterator.xt[iterator.xt.Count - 1];
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
                        tmprms= tmprms / cluster[i].Length;
                        rms[i] += tmprms;
                    }

                    //ljapunow berechnung
                }
                //rms berechnung
                for (int i = 0; i < rms.Length; i++)
                {
                    rms[i] /= rec;
                    rms[i] = Math.Sqrt(rms[i]);
                }
              
                //ljapunow berechnung

                //ausgab
                result ret_result = new result(beta, rms, ljapunow);
                callback(ret_result);
            }
        }

    }
}

