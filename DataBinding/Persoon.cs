using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    public class Persoon
    {
        public string Naam { get; set; }
        public Persoon(string naam)
        {
            Naam = naam;
        }
    }
}
