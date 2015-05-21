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
            return String.Format("<tr><td>{0}</td>{1}</tr>", Standort.ToString(), Wetterwert.ToString());
        }
    }
}