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
        public event PropertyChangedEventHandler PropertyChanged; 
        public string ID { get; set; }
        private int _Value;
        public int Value
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
        public SolidColorBrush Brush
        {
            get
            {
                //im moment nur rotkanal je nach wert anders.
                return new SolidColorBrush(Color.FromRgb(Convert.ToByte(_Value), 0, 0));
            }
        }

        public Vertex(string id, int value)
        {
            ID = id;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", ID, Value);
        }
    }
}