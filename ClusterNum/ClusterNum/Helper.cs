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
                    intsplitarr = Array.ConvertAll(strsplitarr, s=>Double.Parse(s,System.Globalization.CultureInfo.InvariantCulture));
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
        //berechnet transformation nach schwerpunkt/relativ-koordinaten
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
            //gibt eingespeicherte tmat zurück
            string tmattext;
            switch (network)
            {
                case 0:
                    tmattext = 
@"1.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 
0.0 1.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 
0.0 0.0 1.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 
0.0 0.0 0.0 1.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 
0.0 0.0 0.0 0.0 1.0 0.0 0.0 0.0 0.0 0.0 0.0 
0.0 0.0 0.0 0.0 0.0 1.0 0.0 0.0 0.0 0.0 0.0 
0.0 0.0 0.0 0.0 0.0 0.0 1.0 0.0 0.0 0.0 0.0 
0.0 0.0 0.0 0.0 0.0 0.0 0.0 1.0 0.0 0.0 0.0 
0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 1.0 0.0 0.0 
0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 1.0 0.0 
0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 1.0";
                    break;
                case 1:
                    tmattext =
@"0 0 0 0 -0.707106781186548 0 0 0 0 -0.707106781186548 0 
0 0 0 -0.707106781186548 0 -0.707106781186548 0 0 0 0 0 
0 0 0 0 0 0 0 0 0 0 1 
-0.707106781186548 0 0 0 0 0 0 -0.707106781186548 0 0 0 
0 -0.5 -0.5 0 0 0 -0.5 0 -0.5 0 0 
0 0 0 -0.707106781186548 0 0.707106781186548 0 0 0 0 0 
0 -0.5 0.5 0 0 0 -0.5 0 0.5 0 0 
0 0 0 0 -0.707106781186548 0 0 0 0 0.707106781186548 0 
-0.707106781186548 0 0 0 0 0 0 0.707106781186548 0 0 0 
0 0 0.707106781186548 0 0 0 0 0 -0.707106781186548 0 0 
0 -0.707106781186548 0 0 0 0 0.707106781186548 0 0 0 0";
                    break;
                case 2:
                    tmattext = 
@"0.0 0.0 0.0 -1.0 -0.0 0.0 -0.0 -0.0 0.0 0.0 -0.0 
-0.0 -0.0 0.5 0.0 0.5 0.0 0.0 0.0 0.5 0.5 0.0 
-0.40825 -0.40825 0.0 0.0 0.0 -0.40825 -0.40825 -0.40825 0.0 0.0 -0.40825 
-0.0 -0.0 -0.5 0.0 0.5 -0.0 -0.0 -0.0 0.5 -0.5 -0.0 
-0.0 -0.0 -0.0 -0.0 -0.70711 0.0 -0.0 -0.0 0.70711 -0.0 -0.0 
-0.0 -0.0 -0.70711 0.0 -0.0 -0.0 -0.0 -0.0 -0.0 0.70711 -0.0 
-0.0 -0.11419 -0.0 0.0 0.0 -0.08909 0.79679 -0.58661 0.0 0.0 -0.0069 
0.0 -0.25428 -0.0 -0.0 0.0 -0.26574 0.40518 0.64647 0.0 0.0 -0.53163 
0.0 0.19331 -0.0 0.0 0.0 0.68911 -0.03097 -0.17641 0.0 0.0 -0.67504 
0.0 -0.82761 0.0 -0.0 -0.0 0.49656 0.0 0.08276 0.0 -0.0 0.24828 
-0.91287 0.18257 -0.0 -0.0 -0.0 0.18257 0.18257 0.18257 0.0 -0.0 0.18257";
                    break;
                default:
                    throw new NotSupportedException("noch nicht implementiert");
            }
            return Helper.MatrixFromString(tmattext);
        }
    }
}
