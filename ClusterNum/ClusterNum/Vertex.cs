﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Media;
using System.ComponentModel;


namespace ClusterNum
{
    //eigener Vertex. Entspricht einem knoten des graphens mit dessen wert "Value".


    [DebuggerDisplay("{ID}-{Value}")]
    public class Vertex : INotifyPropertyChanged
    {
        private static Color[] cluster_colors = new Color[] { Colors.Red,Colors.Blue,Colors.Green,Colors.Yellow,Colors.Indigo };

        public event PropertyChangedEventHandler PropertyChanged;
        public string ID { get; set; }
        private int _Cluster;
        public int Cluster
        {
            get { return _Cluster; }
            set
            {
                _Cluster = value;
                //benachrichtigung damit binding aktualisiert wird
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Brush"));
            }

        }
        private double _Value;
        public double Value
        {
                        get { return _Value; }
            set
            {
                //_Value = Math.Min(Math.Max(-1,value),1);
                //benachrichtigung damit binding aktualisiert wird
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Brush"));
            }

        }
        public SolidColorBrush Brush
        {
            get
            {
                Color basecolor = cluster_colors[_Cluster % cluster_colors.Length];
                Color col = Color.Multiply(basecolor, (float)_Value);
                col.A = 255;
                return new SolidColorBrush(col);

                //int offset = 255;
                //return new SolidColorBrush(Color.FromRgb(Convert.ToByte(Math.Min(Math.Max(0, basecolor.R +offset* modifier),255)), Convert.ToByte(Math.Min(Math.Max(0, basecolor.G +offset* modifier),255)),Convert.ToByte(Math.Min(Math.Max(0, basecolor.B +offset* modifier),255))));
                
            }
        }

        public Vertex(string id, int value, int cluster)
        {
            ID = id;
            Value = value;
            Cluster = cluster;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", ID, Value);
        }
    }
}