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




        public double betamin, betamax, sigma, delta, pertubation;
        public int betasteps, pre, rec;
        private NumIterator iterator;
        private NumIterator pertiterator;
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

        public NumVariator(double[,] adjMatrix, double betamin, double betamax, int betasteps, double sigma, double delta, double pertubation, int pre, int rec, int[][] cluster, Action<result> callback)
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

            /*
            string clusterstring = "";
            for (int m = 0; m < clusterTransform.Length; m++)
            {
                string s = "";
                for (int i = 0; i < clusterTransform[m].Length; i++)
                    s += clusterTransform[m][i].ToString() + "|";
                clusterstring += s + "\n";
            }
            MessageBox.Show(clusterstring);
            MessageBox.Show(komischeMatrix.ToString(DefaultMatrixFormatProvider.CurrentCulture), "komische Mat");
            MessageBox.Show(BMat.ToString(DefaultMatrixFormatProvider.CurrentCulture), "BMat");*/
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

                NumIterator smIterator = new NumIterator(adjmatrix, beta, sigma, delta);

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
                            if (!double.IsNaN(punator.ljapunowSum[etanodenum]))
                            {
                                ljapunow[m] = Math.Max(punator.ljapunowSum[etanodenum], ljapunow[m]);
                            }
                        }
                    }
                }
                for (int m = 0; m < clusterTransform.Length; m++)
                {
                    ljapunow[m] /= (double)rec;
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
        //private void tmat()
        //{

        //    int numreal = 0;
        //    for (int i = 0; i < cluster.Length; i++)
        //    {
        //        if (cluster[i].Length > 1) numreal++;
        //    }
        //    int[][] realcluster = new int[numreal][];
        //    int k = 0;
        //    for (int i = 0; i < cluster.Length; i++)
        //    {
        //        if (cluster[i].Length > 1)
        //        {
        //            realcluster[k++] = cluster[i];
        //        }

        //    }

        //    double[][][] pmat = new double[realcluster.Length + 1][][];
        //    pmat[0] = new double[nodeCount][];
        //    double[,] tmat = new double[nodeCount, nodeCount];

        //    //PMat.0 erstellen
        //    for (int icluster = 0; icluster < cluster.Length; icluster++)
        //    {
        //        double[] row = new double[nodeCount];
        //        double number = 1.0 / cluster[icluster].Length;
        //        for (int inode = 0; inode < cluster[icluster].Length; inode++)
        //        {
        //            int nodenum = cluster[icluster][inode];
        //            row[nodenum] = number;
        //        }
        //        for (int inode = 0; inode < cluster[icluster].Length; inode++)
        //        {
        //            int nodenum = cluster[icluster][inode];
        //            pmat[0][nodenum] = row;
        //        }
        //    }
        //    //restliche Pmats für die einzelnen Cluster
        //    for (int ipmat = 1; ipmat < pmat.Length; ipmat++)
        //    {
        //        pmat[ipmat] = new double[nodeCount][];
        //        for (int j = 0; j < nodeCount; j++)
        //        {
        //            pmat[ipmat][j] = new double[nodeCount];
        //        }

        //        int[] currentClust = realcluster[ipmat - 1];
        //        double number = 1.0 / (double)currentClust.Length;
        //        double vorzeichen = 1;
        //        for (int inoderow = 0; inoderow < currentClust.Length; inoderow++)
        //        {
        //            int row=currentClust[inoderow];
        //            for (int inodecol = 0; inodecol < currentClust.Length; inodecol++)
        //            {
        //                int col=currentClust[inodecol];
        //                pmat[ipmat][row][col] = 1;
        //            }
        //        }

        //        MessageBox.Show(pmat[ipmat].ToString(DefaultMatrixFormatProvider.CurrentCulture));
        //    }

        //    //  


        //}
    }

}