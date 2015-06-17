using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtParkeerBon.Model
{
    public class BonInfo
    {
        private DateTime datumValue;

        private DateTime aankomstTijdValue;

        private DateTime vertrekTijdValue;

        public DateTime Datum
        {
            get
            { return datumValue; }
            set
            { datumValue = value; }
        }
        public DateTime AankomstTijd
        {
            get
            { return aankomstTijdValue; }
            set
            { aankomstTijdValue = value; }
        }

        public decimal Bedrag { get; set; }

        public DateTime VertrekTijd
        {
            get
            { return vertrekTijdValue; }
            set
            { vertrekTijdValue = value; }
        }
    }
}
