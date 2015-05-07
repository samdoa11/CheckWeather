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
            return String.Format("<table><tr><td>Temperatur</td><td>Luftfeuchtigkeit</td>"
                +"<td>Windgeschwindigkeit</td><td>Windrichtung</td><td>Niederschlag</td>"
                +"<td>Datum</td><td>Taupunkt</td><td>Windspitze</td><td>Luftdruck Meeresniveau</td>"
                +"<td>Luftdruck Stationsniveau</td><td>Sonneneinstrahlung</td></tr><tr>"
                +"<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td>"
                +"<td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td></tr>"
                +"</table>",Temperatur,RelativeLuftfeuchtigkeit,Windgeschwindigkeit,Windrichtung,Niederschlag,
                Messdatum,Taupunkt,Windspitze,LuftdurckMeeresniveau,LuftdruckStationsniveau,Sonneneinstrahlung);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}