using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataBinding
{
    public class Persoon : INotifyPropertyChanged
    {
        private string naamValue;
        public string Naam
        {
            get
            {
                return naamValue;
            }
            set
            {
                naamValue = value;
                RaisePropertyChanged("Naam");
            }
        }
        public Persoon(string naam)
        {
            Naam = naam;
        }

        public void RaisePropertyChanged(string info)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
