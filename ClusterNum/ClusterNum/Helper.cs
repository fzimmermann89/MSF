using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClusterNum
{
    public class Helper
    {
        public static double[,] MatrixFromString(string matrixstring)
        {


            string[] strarr = matrixstring.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int nodeCount = strarr.Length;
            double[,] retmat = new double[nodeCount,nodeCount];

            int i = 0;
            foreach (string line in strarr)
            {
                string[] strsplitarr = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (strsplitarr.Length != nodeCount)
                {
                    MessageBox.Show("Matrix ist nicht quadratisch", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                double[] intsplitarr;
                try
                {
                    intsplitarr = Array.ConvertAll(strsplitarr, Double.Parse);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Matrix ungültig:\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
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

                string[] parts = matches[j].Groups[1].Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int iparts = 0; iparts < parts.Length; iparts++)
                {
                    string[] subparts = parts[iparts].Split(':');
                    if (subparts.Length > 1)
                    {
                        parts[iparts] = "";
                        for (int irangepos = int.Parse(subparts[0]); irangepos < int.Parse(subparts[1]); irangepos++)
                        {
                            parts[iparts] += irangepos.ToString() + " ";
                        }
                        parts[iparts] += int.Parse(subparts[1]);
                    }
                }
                cluster[j] = Array.ConvertAll(parts, int.Parse);
            }
            return cluster;
        }
    }
}
