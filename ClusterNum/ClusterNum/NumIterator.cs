using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterNum
{
    public class NumIterator
    {
        public static Random rand = new Random();

        public int vertexCount;
        public int[][] adjMatrix;

        public double beta, sigma, delta;

        public List<double[]> xt = new List<double[]>();
        public double pertubation = 0.01;

        public NumIterator(int[][] adjMatrix, double beta, double sigma, double delta)
        {
            this.beta = beta;
            this.sigma = sigma;
            this.delta = delta;
            this.adjMatrix = adjMatrix;
            this.vertexCount = adjMatrix.Length;
            xt.Add(new double[vertexCount]);

        }

        public void iterate()
        {
            double[] oldxi = xt[xt.Count - 1];

            double[] pertIntensity = new double[vertexCount];
            for (int i = 0; i < pertIntensity.Length; i++)
            {
                //double err = (rand.NextDouble() - 0.5) * 2.0 * pertubation;
                double u1 = rand.NextDouble(); 
                double u2 = rand.NextDouble();
                double randnormal = Math.Sqrt(-2.0 * Math.Log(u1)) *  Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                double err = pertubation * randnormal;

                pertIntensity[i] = intensity(oldxi[i]);
                pertIntensity[i] += err;
            }

            double[] newxi = new double[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < vertexCount; j++)
                {
                    sum += (double)adjMatrix[i][j] * pertIntensity[j];
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


        public double intensity(double x)
        {
            double ret = (1 - Math.Cos(x)) / 2.0;
            return ret;
        }
    }
}
