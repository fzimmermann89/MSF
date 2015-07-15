using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;



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
        public Action<int, int> sigmaVariationCallback;

        double[,] TMat;
        double[,] TMatInverse;
        double[,] BMat;
        double[][,] EMats;
        double[][,] JMats;
        int[][] clusterTransform;

        int nodeCount;

        Random rand = new Random();

        public NumVariator(double[,] adjMatrix, double[,] TMat, double betamin, double betamax, int betasteps, double sigma, double delta, double noise, double pert, int pre, int rec, int[][] cluster, Action<result> callback)
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
            this.TMat = TMat;

            // Zeugs für BMat, JMats und clusterTransform berechnung, wird übergeben an Ljapunator
            TMatInverse = TMat.Inverse().Round(5);
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

            BMat = TMat.Multiply(adjmatrix).Multiply(TMatInverse).Round(5);
            // MessageBox.Show(BMat.ToString(DefaultMatrixFormatProvider.CurrentCulture)); //bmat anzeigen. hilfreich für debug
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

        public double[][] VariateBeta(double _betamin, double _betamax, int _betasteps, double _sigma)
        {
            // [beta][m] (m: Cluster)
            double[][] ljapunow = new double[_betasteps+1][];
            for (int ibeta = 0; ibeta <= _betasteps; ibeta++)
            {
                double _beta = _betamin + ibeta * (_betamax - _betamin) / _betasteps;

                //Ljapunow 
                //synchrone orbits berechnen

                NumIterator smIterator = new NumIterator(adjmatrix, _beta, _sigma, delta, pertubation);
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

                ljapunow[ibeta] = new double[cluster.Length];
                ljapunow[ibeta] = ljapunow[ibeta].Add(Double.MinValue);

                for (int m = 0; m < clusterTransform.Length; m++)
                {

                    for (int i = 0; i < clusterTransform[m].Length; i++)
                    {
                        int etanodenum = clusterTransform[m][i];
                        if (etanodenum >= clusterTransform.Length) // unterer Block
                        {
                            Ljapunator punator = new Ljapunator(JMats, BMat, cluster.Length, smts, _beta, _sigma, delta);
                            punator.etat[0][etanodenum] = pertubation;
                            punator.iterate(rec);
                            ljapunow[ibeta][m] = Math.Max(punator.ljapunowSum / (double)rec, ljapunow[ibeta][m]);
                        }
                    }
                }


            }
            return ljapunow;
        }

        public double[][][] VariateSigmaBeta(double _betamin, double _betamax, int _betasteps, double _sigmamin, double _sigmamax, int _sigmasteps)
        {

            // etwas dämlich aber so war es am einfachsten
            // [sigma][beta][m] (m: Cluster)
            double[][][] ljapunow = new double[_sigmasteps+1][][];
            Parallel.For(0, _sigmasteps + 1, isigma =>
            {

                double _sigma = _betamin + isigma * (_sigmamax - _sigmamin) / _sigmasteps;


                // [beta][m] (m: Cluster)
                double[][] tmpLjapunow = VariateBeta(_betamin, _betamax, _betasteps, _sigma);

                ljapunow[isigma] = tmpLjapunow;
                if (sigmaVariationCallback != null)
                    sigmaVariationCallback(isigma, _sigmasteps);
            });

            return ljapunow;
        }

        public void DoWork()
        {

            Parallel.For(0, betasteps + 1, ibeta =>
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
                            Ljapunator punator = new Ljapunator(JMats, BMat, cluster.Length, smts, beta, sigma, delta);
                            punator.etat[0][etanodenum] = pertubation;
                            punator.iterate(rec);
                            ljapunow[m] = Math.Max(punator.ljapunowSum / (double)rec, ljapunow[m]);

                        }
                    }
                }

                result ret_result = new result(beta, rms, ljapunow);
                callback(ret_result);

            });
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

