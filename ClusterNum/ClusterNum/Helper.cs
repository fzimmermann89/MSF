using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Accord.Math;

namespace ClusterNum
{
    public class Helper
    {
        public static string[] adjmatrix ={
@"0 1 1 1 1 1 0 1 1 1 1
1 0 1 0 1 1 1 1 0 1 1
1 1 0 1 1 1 0 1 1 1 0
1 0 1 0 1 1 1 1 1 1 0
1 1 1 1 0 1 1 1 1 0 1
1 1 1 1 1 0 0 1 1 0 1
0 1 0 1 1 0 0 1 1 1 1
1 1 1 1 1 1 1 0 1 1 1
1 0 1 1 1 1 1 1 0 1 1
1 1 1 1 0 0 1 1 1 0 1
1 1 0 0 1 1 1 1 1 1 0",
@"0 1 1 1 0 1 1 1 1 1 1
1 0 1 1 1 1 0 1 1 1 1
1 1 0 1 1 1 1 1 0 1 1
1 1 1 0 1 1 1 1 1 1 1
0 1 1 1 0 1 1 1 1 1 0
1 1 1 1 1 0 1 1 1 1 1
1 0 1 1 1 1 0 1 1 1 1
1 1 1 1 1 1 1 0 1 0 1
1 1 0 1 1 1 1 1 0 1 1
1 1 1 1 1 1 1 0 1 0 0
1 1 1 1 0 1 1 1 1 0 0",
@"0 1 1 1 1 1 1 1 1 1 1
1 0 1 1 1 1 1 1 1 1 1
1 1 0 0 1 1 1 1 1 0 1
1 1 0 0 0 1 1 1 0 0 1
1 1 1 0 0 1 1 1 0 1 1
1 1 1 1 1 0 1 1 1 1 1
1 1 1 1 1 1 0 1 1 1 1
1 1 1 1 1 1 1 0 1 1 1
1 1 1 0 0 1 1 1 0 1 1
1 1 0 0 1 1 1 1 1 0 1
1 1 1 1 1 1 1 1 1 1 0"};
        public static double[,] MatrixFromString(string matrixstring)
        {


            string[] strarr = matrixstring.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int nodeCount = strarr.Length;
            double[,] retmat = new double[nodeCount, nodeCount];

            int i = 0;
            foreach (string line in strarr)
            {
                string[] strsplitarr = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (strsplitarr.Length != nodeCount)
                {
                    throw new FormatException("Matrix ist nicht quadratisch");

                }
                double[] intsplitarr;
                try
                {
                    intsplitarr = Array.ConvertAll(strsplitarr, Double.Parse);
                }
                catch (FormatException ex)
                {
                    throw new FormatException("Matrix ungültig:\n" + ex.Message);
                }

                for (int j = 0; j < nodeCount; j++)
                {
                    retmat[i, j] = intsplitarr[j];
                }
                i++;

            }
            return retmat;
        }
        public static int[][] dreadnaut2cluster(string ergebnis)
        {
            string[] s = ergebnis.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            MatchCollection matches = Regex.Matches(s[s.Length - 2], @"([0-9: ]+)(\(([^)]*)\))?;");
            int[][] cluster = new int[matches.Count][];

            for (int j = 0; j < matches.Count; j++)
            {
                string match = matches[j].Groups[1].Value;
                MatchCollection repmatches = Regex.Matches(match, "([0-9]+):([0-9]+)");
                foreach (Match repmatch in repmatches)
                {
                    for (int irangepos = int.Parse(repmatch.Groups[1].Value) + 1; irangepos < int.Parse(repmatch.Groups[2].Value); irangepos++)
                    {
                        match += " " + irangepos.ToString();
                    }
                }

                string[] parts = match.Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);

                cluster[j] = Array.ConvertAll(parts, int.Parse);
                Array.Sort(cluster[j]);
            }
            return cluster;
        }
        public static double[,] TMat(int[][] cluster)
        //berechnet transformation nach schwerpunkt/relativ-koordinaten wenn calculate==true, ansosnten gib matrix für graph2 zurück
        {
            int nodecount = 0;
            foreach (int[] curcluster in cluster) nodecount += curcluster.Length;
            double[,] tmat = new double[nodecount, nodecount];


            //oberer block-schwerpunkt->synchrone bewegung
            for (int i = 0; i < cluster.Length; i++)
            {
                double value = 1.0 / (cluster[i].Length);
                for (int j = 0; j < cluster[i].Length; j++)
                {
                    int node = cluster[i][j];
                    tmat[i, node] = value;
                }
            }

            //unterer block->abweichungen der knoten vom mittelwert
            int row = cluster.Length;
            for (int i = 0; i < cluster.Length; i++)
            {
                for (int j = 0; j < cluster[i].Length - 1; j++)
                {
                    for (int k = 0; k < cluster[i].Length; k++)
                    {
                        int negnode = cluster[i][k];
                        tmat[row, negnode] = -1.0 / (cluster[i].Length - 1);
                    }
                    int posnode = cluster[i][j];
                    tmat[row, posnode] = 1;
                    row++;
                }
            }


            return tmat;
        }
        public static double[,] TMat(int network)
        {
            double[,] tmat = new double[11, 11];
            switch (network)
            {

                case 1:
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
                    tmat = Helper.MatrixFromString(tmattext);
                    break;
                default:
                    throw new NotSupportedException("noch nicht implementiert");
            }
            return tmat;
        }
    }
}
