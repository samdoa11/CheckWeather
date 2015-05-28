using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    /// <summary>
    /// Klasse, die Werte für den Standort der Wetterstationen
    /// verwaltet.
    /// Autor: Lisa
    /// </summary>
    public class Standort
    {
        public int PLZ { get; set; }
        public String Ort { get; set; }
        public String Strasse { get; set; }
        public double xKoordinate { get; set; }
        public double yKoordinate { get; set; }
        public double Seehoehe { get; set; }

        public override string ToString()
        {

            return String.Format("{0}", Ort);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}