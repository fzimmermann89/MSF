using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;
using System.Windows.Forms;

namespace ClusterNum
{
    public class Ljapunator
    {


        public static Random rand = new Random();

        public double[,] adjMatrix;



        public double beta, sigma, delta;

        public List<double[]> etat = new List<double[]>();
        public double pertubation = 0.00;

        double[,] TMat;
        double[,] TMatInverse;
        double[,] BMat;
        double[][,] EMats;
        double[][,] JMats;
        List<double[]> smts;

        int[][] cluster;
        int[][] clusterTransform;

        int dim;

        public double[] ljapunowSum;

        public Ljapunator(double[,] adjMatrix, double[,] TMatrix, int[][] cluster, List<double[]> smts, double beta, double sigma, double delta)
        {
            this.beta = beta;
            this.sigma = sigma;
            this.delta = delta;
            this.adjMatrix = adjMatrix;
            this.dim = adjMatrix.GetLength(0);
            etat.Add(new double[dim]);

            ljapunowSum = new double[dim];

            this.smts = smts;

            this.cluster = cluster;

            TMat = TMatrix;
            TMatInverse = TMat.Inverse();
            EMats = new double[cluster.Length][,];
            JMats = new double[cluster.Length][,];
            for (int ci = 0; ci < cluster.Length; ci++)
            {
                EMats[ci] = new double[dim, dim];

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

            BMat = TMat.Multiply(adjMatrix).Multiply(TMatInverse);

            double[,] komischeMatrix = new double[dim, dim];
            for (int i = 0; i < dim; i++)
            {
                for (int m = 0; m < cluster.Length; m++)
                {
                    if (JMats[m][i, i] != 0)
                    {
                        komischeMatrix[i, i] = m;
                    }
                }
            }
            

            //MessageBox.Show(komischeMatrix.ToString(DefaultMatrixFormatProvider.CurrentCulture), "komische Mat");
            //MessageBox.Show(BMat.ToString(DefaultMatrixFormatProvider.CurrentCulture), "BMat");
        }


        public void iterate()
        {

            int tIndex = etat.Count - 1;

            double[] oldetai = etat[etat.Count - 1];
            double[] newetai = new double[dim];

            double[,] sumJDF = new double[dim, dim];
            double[,] sumJDH = new double[dim, dim];

            for (int clusternum = 0; clusternum < cluster.Length; clusternum++)
            {

                double DFsmt = beta * dintensity(smts[tIndex][clusternum]);
                double DHsmt = sigma * dintensity(smts[tIndex][clusternum]);

                sumJDF = sumJDF.Add(DFsmt.Multiply(JMats[clusternum]));
                sumJDH = sumJDH.Add(DHsmt.Multiply(JMats[clusternum]));

            }
            //MessageBox.Show(oldetai.ToString(DefaultMatrixFormatProvider.CurrentCulture),"Current Eta");
            //MessageBox.Show(sumJDF.ToString(DefaultMatrixFormatProvider.CurrentCulture),"Current Sum J*DF");
            //MessageBox.Show(sumJDH.ToString(DefaultMatrixFormatProvider.CurrentCulture),"Current Sum J*DH");

            newetai = sumJDF.Multiply(oldetai);
            newetai = newetai.Add(BMat.Multiply(sumJDH).Multiply(oldetai));
            // newetai = newetai.Add(oldetai);
            double[] tmp = newetai.ElementwiseDivide(oldetai).Abs().Log();
            for (int k = 0; k < ljapunowSum.Length; k++)
            {
                ljapunowSum[k] += tmp[k];
            }
            double oldetalength = oldetai.Euclidean();
            double newetalength = newetai.Euclidean();
            double scale = oldetalength / newetalength;
            newetai = scale.Multiply(newetai);


            // etaratio.
            etat.Add(newetai);
        }
        public void iterate(int numIteration)
        {
            for (int i = 0; i < numIteration; i++)
            {
                iterate();
            }
        }




        private double intensity(double x)
        {
            double ret = (1 - Math.Cos(x)) / 2.0;
            return ret;
        }
        public double dintensity(double x)
        {
            double ret = (Math.Sin(x)) / 2.0;
            return ret;
        }

        private double[] intensities(double[] xi)
        {
            double[] pertIntensity = new double[dim];
            for (int i = 0; i < pertIntensity.Length; i++)
            {

                //double err = (rand.NextDouble() - 0.5) * 2.0 * pertubation;
                double u1 = rand.NextDouble();
                double u2 = rand.NextDouble();
                double randnormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double err = pertubation * randnormal;

                pertIntensity[i] = intensity(xi[i]);
                pertIntensity[i] += err;
            }
            return pertIntensity;

        }
        private void tmat(int[][] cluster)
        {

            double[,] tmat = new double[dim, dim];
            //TODO: berechnung der Tmat aus den Clustern
        }
    }

}
