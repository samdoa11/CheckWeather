using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class Wetterstation
    {
        public Standort Standort { get; set; }
        public Wetterwert Wetterwert { get; set; }

        public override string ToString()
        {
            return Standort.ToString() + Wetterwert.ToString();
        }
    }
}