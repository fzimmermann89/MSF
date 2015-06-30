using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;

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
        int[][] cluster;

        int dim;


        public Ljapunator(double[][] adjMatrix,double[][] TMatrix,int[][] cluster, double beta, double sigma, double delta)
        {
            this.beta = beta;
            this.sigma = sigma;
            this.delta = delta;
            this.adjMatrix = adjMatrix;
            this.vertexCount = adjMatrix.Length;
            etat.Add(new double[vertexCount]);

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
        public Ljapunator(double[][] adjMatrix, double beta, double sigma, double delta)
        {
            this.beta = beta;
            this.sigma = sigma;
            this.delta = delta;
            this.adjMatrix = adjMatrix;
            this.vertexCount = adjMatrix.Length;
            etat.Add(new double[vertexCount]);

        }

        public void iterate()
        {
            double[] oldxi = etat[etat.Count - 1];
            double[] newxi = new double[vertexCount];

            double[][] sumJDF;
            double[][] sumJDH;

            //double[] DF

            etat.Add(newxi);
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
    }
}
