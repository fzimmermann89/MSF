using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClusterNum
{
    public class Helper
    {
        public static double[][] MatrixFromString(string matrixstring)
        {


            string[] strarr = matrixstring.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int dim = strarr.Length;
            double[][] retmat = new double[dim][];

            int i = 0;
            foreach (string line in strarr)
            {
                string[] strsplitarr = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (strsplitarr.Length != dim)
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
                    MessageBox.Show("Matrix ungültig:\n" +ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

               
                retmat[i++] = intsplitarr;

            }
            return retmat;
        }
    }
}
