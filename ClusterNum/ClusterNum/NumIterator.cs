using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterNum
{
    public class NumIterator
    {
        int vertexCount;
        int[][] adjMatrix;

        double beta, sigma, delta;

        List<double[]> xt = new List<double[]>();

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
            double[] oldxi=xt[xt.Count-1];
            double[] newxi = new double[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
               
                double sum = 0;
                for (int j = 0; j < vertexCount; j++)
                {
                    sum += (double)adjMatrix[i][j] * intensity(oldxi[j]);
                }
                double tmp = beta * intensity(oldxi[i]) + sigma * sum + delta;
                newxi[i] = tmp % (2.0 * Math.PI);
            }

            xt.Add(newxi);
        }

        public static double intensity(double x)
        {
            return (1 - Math.Cos(x)) / 2.0;
        }
    }
}
