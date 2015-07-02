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

        public int vertexCount;
        public double[][] adjMatrix;

        public double beta, sigma, delta;

        public List<double[]> etat = new List<double[]>();
        public double pertubation = 0.00;

        double[][] TMat;
        double[][] TMatInverse;
        double[][] BMat;
        double[][][] EMats;
        double[][][] JMats;

        List<double[]> smts;

        int[][] cluster;

        int dim;

        public  List<double[]> etaratio=new   List<double[]>();

        public Ljapunator(double[][] adjMatrix, double[][] TMatrix, int[][] cluster, List<double[]> smts, double beta, double sigma, double delta)
        {
            this.beta = beta;
            this.sigma = sigma;
            this.delta = delta;
            this.adjMatrix = adjMatrix;
            this.vertexCount = adjMatrix.Length;
            etat.Add(new double[vertexCount]);

            this.smts = smts;

            this.cluster = cluster;
            this.dim = adjMatrix.Length;
            TMat = TMatrix;
            TMatInverse = TMat.Inverse();
            EMats = new double[cluster.Length][][];
            JMats = new double[cluster.Length][][];
            for (int ci = 0; ci < cluster.Length; ci++)
            {
                EMats[ci] = new double[dim][];
                for (int i = 0; i < EMats[ci].Length; i++)
                {
                    EMats[ci][i] = new double[dim];
                }
            }
            for (int ci = 0; ci < cluster.Length; ci++)
            {
                for (int j = 0; j < cluster[ci].Length; j++)
                {
                    int nodenum = cluster[ci][j];
                    EMats[ci][nodenum][nodenum] = 1.0;
                }
                JMats[ci] = TMat.Multiply(EMats[ci]).Multiply(TMatInverse);
            }

            BMat = TMat.Multiply(adjMatrix).Multiply(TMatInverse);


        }


        public void iterate()
        {

            int tIndex = etat.Count - 1+1;

            double[] oldetai = etat[etat.Count - 1];
            double[] newetai = new double[vertexCount];

            double[][] sumJDF = new double[vertexCount][];
            double[][] sumJDH = new double[vertexCount][];
            for (int i = 0; i < vertexCount; i++)
            {
                sumJDF[i] = new double[vertexCount];
                sumJDH[i] = new double[vertexCount];
            }
            for (int clusternum = 0; clusternum < cluster.Length; clusternum++)
            {
                /*
                double[][] DFsmt = new double[vertexCount][];
                double[][] DHsmt = new double[vertexCount][];
                for (int i = 0; i < vertexCount; i++)
                {
                    DFsmt[i] = new double[vertexCount];
                    DHsmt[i] = new double[vertexCount];
                    DFsmt[i][i] = (beta * dintensity(smts[tIndex][i])) % (2.0 * Math.PI);
                    DHsmt[i][i] = (sigma * dintensity(smts[tIndex][i])) % (2.0 * Math.PI);

                }*/
                double DFsmt = beta * dintensity(smts[tIndex][clusternum]);
                double DHsmt = sigma * dintensity(smts[tIndex][clusternum]);

                double[,] njmat1=JMats[clusternum].ToMatrix();
                njmat1=DFsmt.Multiply(njmat1);
                double[][] addmat1=njmat1.ToArray();

                double[,] njmat2 = JMats[clusternum].ToMatrix();
                njmat2 = DHsmt.Multiply(njmat2);
                double[][] addmat2 = njmat2.ToArray();

                sumJDF = sumJDF.Add(addmat1);
                sumJDH = sumJDH.Add(addmat2);

            }
            //MessageBox.Show(oldetai.ToString(DefaultMatrixFormatProvider.CurrentCulture),"Current Eta");
            //MessageBox.Show(sumJDF.ToString(DefaultMatrixFormatProvider.CurrentCulture),"Current Sum J*DF");
            //MessageBox.Show(sumJDH.ToString(DefaultMatrixFormatProvider.CurrentCulture),"Current Sum J*DH");

            newetai = sumJDF.Multiply(oldetai);
            newetai = newetai.Add(BMat.Multiply(sumJDH).Multiply(oldetai));
            newetai.Add(oldetai);
           double[] tmp= newetai.ElementwiseDivide(oldetai);
           etaratio.Add(tmp);

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
            double[] pertIntensity = new double[vertexCount];
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
            
            double[,] tmat=new double[dim,dim];
            //TODO: berechnung der Tmat aus den Clustern
        }
    }
 
}
