using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Media;
using System.ComponentModel;

namespace ProjectMSF
{
   //eigener Vertex. Entspricht einem knoten des graphens mit dessen wert "Value".

    [DebuggerDisplay("{ID}-{Value}")]
    public class Vertex : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; 
        public string ID { get; set; }
        private double _Value;
        private Color _color;
        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                //benachrichtigung damit binding aktualisiert wird
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Brush"));
            }

        }
        public Color color
        {
            get { return _color; }
            set
            {
                _color = value;
                //benachrichtigung damit binding aktualisiert wird
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Brush"));
            }

        }
        public SolidColorBrush Brush
        {
            get
            {
                //im moment nur rotkanal je nach wert anders.
                Color col = Color.Multiply(color, (float)_Value);
                col.A = 255;
                return new SolidColorBrush(col);
            }
        }

        public Vertex(string id,Color col, double value)
        {
            ID = id;
            Value = value;
            color = col;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", ID, Value);
        }
    }
}