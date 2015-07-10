using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;

namespace ClusterNum
{
    public class NumIterator
    {
        public static Random rand = new Random();

        public int nodeCount;
        public double[,] adjMatrix;

        public double beta, sigma, delta;

        public List<double[]> xt = new List<double[]>();
        public double noise = 0.0;

        public NumIterator(double[,] adjMatrix, double beta, double sigma, double delta,double pertubation)
        {
            this.beta = beta;
            this.sigma = sigma;
            this.delta = delta;
            this.adjMatrix = adjMatrix;
            this.nodeCount = adjMatrix.GetLength(0);
            xt.Add(new double[nodeCount]);
            xt[0] = pertubate(xt[0], pertubation).Add(Math.PI);

        }

        public void iterate()
        {
            double[] oldxi = xt[xt.Count - 1];
            double[] pertIntensity = intensities(oldxi);
            double[] newxi = new double[nodeCount];

            for (int i = 0; i < nodeCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < nodeCount; j++)
                {
                    sum += (double)adjMatrix[i,j] * pertIntensity[j];
                }
                double tmp = beta * pertIntensity[i] + sigma * sum + delta;
                newxi[i] = tmp % (2.0 * Math.PI);
            }

            xt.Add(newxi);
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

        public double[] fstrich()
        {
            double[] fstrich = new double[nodeCount];
            double[] xs = xt[xt.Count - 1];
            for (int i = 0; i < nodeCount; i++)
            {
                double tmp = beta * dintensity(xs[i]);
                fstrich[i] = tmp% (2.0 * Math.PI);
            }
            return fstrich;

         


        }

        private double[] intensities(double[] xi)
        {
            double[] pertIntensity = new double[nodeCount];
            for (int i = 0; i < pertIntensity.Length; i++)
            {

                //double err = (rand.NextDouble() - 0.5) * 2.0 * pertubation;
                double u1 = rand.NextDouble();
                double u2 = rand.NextDouble();
                double randnormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double err = noise * randnormal;

                pertIntensity[i] = intensity(xi[i]);
                pertIntensity[i] += err;
            }
            return pertIntensity;

        }

        private double[] pertubate(double[] x, double stdabw)
        {
            double[] pertxi = new double[x.Length];
            Random rand = new Random();
            for (int i = 0; i < x.Length; i++)
            {
                double u1 = rand.NextDouble();
                double u2 = rand.NextDouble();
                double randnormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double err = stdabw * randnormal;
                pertxi[i] = x[i] + err;
            }
            return pertxi;
        }
    }
}
