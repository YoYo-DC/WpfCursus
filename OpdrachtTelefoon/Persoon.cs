using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OpdrachtTelefoon
{
    public class Persoon
    {
        public string Naam { get; set; }
        public string TelefoonNr { get; set; }
        public Groep Groep { get; set; }
        public BitmapImage Foto { get; set; }

        public Persoon(string naam, string telefoonNr, Groep groep, BitmapImage foto)
        {
            Naam = naam;
            TelefoonNr = telefoonNr;
            Groep = groep;
            Foto = foto;
        }
    }
}
