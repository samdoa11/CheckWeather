using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    /// <summary>
    /// Klasse, die Wetterwerte verwaltet, mit denen später
    /// gearbeitet wird.
    /// Autor: Lisa
    /// </summary>
    public class Wetterwert
    {
        public float Temperatur { get; set; }
        public int RelativeLuftfeuchtigkeit { get; set; }
        public float Windgeschwindigkeit { get; set; }
        public int Windrichtung { get; set; }
        public float? Niederschlag { get; set; }
        public DateTime Messdatum { get; set; }
        public float Taupunkt { get; set; }
        public int? Windspitzenrichtung { get; set; }
        public float Windspitzengeschwindigkeit { get; set; }
        public float? LuftdurckMeeresniveau { get; set; }
        public float LuftdruckStationsniveau { get; set; }
        public int Sonneneinstrahlung { get; set; }

        //<tr><td>wert</td><td>wert2</td></tr>

        public override string ToString()
        {
            return String.Format("<table><tr>"
                +"<td>Datum</td>"
                +"<td>Temperatur</td>"
                +"<td>Taupunkt</td>"
                +"<td>Relative Luftfeuchtigkeit</td>"
                +"<td>Windrichtung</td>"
                +"<td>Windgeschwindigkeit</td>"
                +"<td>Windspitzenrichtung</td>"
                +"<td>Windspitzengeschwindigkeit</td>"
                +"<td>Luftdruck Meeresniveau</td>"
                +"<td>Luftdruck Stationsniveau</td>"
                +"<td>Niederschlag</td>"
                +"<td>Sonneneinstrahlung</td></tr>"

                + "<tr>"
                +"<td>{0}</td>" // Datum
                +"<td>{1}</td>" // Temperatur
                +"<td>{2}</td>" // Taupunkt
                +"<td>{3}</td>" // Relative LF
                +"<td>{4}</td>" // Windrichtung
                +"<td>{5}</td>" // Windgeschwindigkeit
                +"<td>{6}</td>" // Windspitzenrichtung
                +"<td>{7}</td>" // Windspitzengeschwindigkeit
                +"<td>{8}</td>" // Luftdruck Meeresniveau
                +"<td>{9}</td>" // LD Stationsniveau
                +"<td>{10}</td>" // Niederschlag
                +"<td>{11}</td>" // Sonneneinstrahlung
                +"</tr>"
                +"</table>",
                
                Messdatum,
                Temperatur,
                Taupunkt,
                RelativeLuftfeuchtigkeit,
                Windrichtung,
                Windgeschwindigkeit,
                Windspitzenrichtung,
                Windspitzengeschwindigkeit,
                LuftdurckMeeresniveau,
                LuftdruckStationsniveau,
                Niederschlag,
                Sonneneinstrahlung
            );
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}