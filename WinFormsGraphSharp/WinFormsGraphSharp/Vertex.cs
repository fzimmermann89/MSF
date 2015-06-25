using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Media;
using System.ComponentModel;


namespace WinFormsGraphSharp
{
    //eigener Vertex. Entspricht einem knoten des graphens mit dessen wert "Value".


    [DebuggerDisplay("{ID}-{Value}")]
    public class Vertex : INotifyPropertyChanged
    {
       private Color[] cluster_colors = new Color[] { Colors.Red,Colors.Blue,Colors.Green,Colors.Yellow,Colors.Indigo };

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
                _Value = Math.Min(Math.Max(-1,value),1);
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
                double modifier = (Value + 2) / 2;
                //vllt nochmal bessere farben überlegen
                
                return new SolidColorBrush(Color.FromRgb(Convert.ToByte(Math.Min(255, basecolor.R * modifier)), Convert.ToByte(Math.Min(255, basecolor.G * modifier)),Convert.ToByte( Math.Min(255, basecolor.B * modifier))));
                
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