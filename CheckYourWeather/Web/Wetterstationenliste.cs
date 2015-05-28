using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Classes;

namespace Web
{
    public class Wetterstationenliste:IList<Wetterstation>
    {
        private List<Wetterstation> list;
        private DAL m_DAL;
        private ServerConnector m_ZamgServer;

        public Wetterstationenliste()
        {
            this.list = new List<Wetterstation>();

            // @Autor: Lisa Schwarz -> Aufruf de ServerConnection Klasse + weitergabe des Links
            this.m_ZamgServer = new ServerConnector("http://www.zamg.ac.at/ogd/");
            String pfad = this.m_ZamgServer.saveCSV();
            this.m_DAL = new DAL(pfad);
            this.ConvertCSV();
            
        }
        public int IndexOf(Wetterstation item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Wetterstation item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Wetterstation this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(Wetterstation item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Wetterstation item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Wetterstation[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(Wetterstation item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Wetterstation> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wandelt die String-Liste in Wetterdaten um.
        /// Author grudoa11
        /// </summary>
        public void ConvertCSV()
        {
            List<String> input = this.m_DAL.readOut();
              foreach (string str in input)
                        {
                            String[] sfeld = str.Split(';');
                            // Station
                            int? stationsnummer = ConvertStringToIntNull(sfeld[0]);
                            string hv = ConvertStringToStringNull(sfeld[1]);
                            string[] hilfsfeld = hv.Split('"');

                            int indexDesOrtes = 0;
                            if (hilfsfeld.Length >= 1) indexDesOrtes = 1; // Falls die Daten von der ZAMG kommen muss Index 1 genommen werden
                            string ortsname = hilfsfeld[indexDesOrtes]; // wegen " und \ war ein Split durchzuführen
                            
                            int? seehoehe = ConvertStringToIntNull(sfeld[2]); // in Meter

                            // Wetterdaten (nach reihenfolge in csv-datei)
                            //DateTime messdatum = Convert.ToDateTime(sfeld[3]); // Datum und Zeit der Messung
                            string[] datum = sfeld[3].Split('-', '"', '.');
                            int indexOfDatum = 0;
                            if (datum.Length >= 3) indexOfDatum = 1; // Falls ZAMG-Datei
                            int tag = Convert.ToInt32(datum[indexOfDatum]);
                            int monat = Convert.ToInt32(datum[indexOfDatum+1]);
                            int jahr = Convert.ToInt32(datum[indexOfDatum+2]);
                            
                            string[] zeit = sfeld[4].Split(':', '"');
                            int indexOfZeit = 0;
                            if (zeit.Length >= 2) indexOfZeit = 1; // Falls ZAMG-Datei
                            int stunde = Convert.ToInt32(zeit[indexOfZeit]);
                            int minute = Convert.ToInt32(zeit[indexOfZeit+1]);
                            DateTime messdatum = new DateTime(jahr, monat, tag, stunde, minute, 0);
                            
                            float? temperatur = ConvertStringToFloatNull(sfeld[5]); // in °C
                            float? taupunkt = ConvertStringToFloatNull(sfeld[6]); // in °C
                            int? relativeLF = ConvertStringToIntNull(sfeld[7]); // in %

                            int? windrichtung = ConvertStringToIntNull(sfeld[8]); // °
                            
                            float? windgeschwindigkeit = ConvertStringToFloatNull(sfeld[9]); // in km/h
                            // Windspitze kann auch NULL sein
                            int? windspitzenrichtung = ConvertStringToIntNull(sfeld[10]);// in °
                            
                            float? windspitzengesch = ConvertStringToFloatNull(sfeld[11]); // in km/h
                            // NULL-Wert möglich
                            float? niederschlag = ConvertStringToFloatNull(sfeld[12]); ;// in l/m²

                            float? luftdruckMeeresniveau = ConvertStringToFloatNull(sfeld[13]);// in hPa (hektoPascal)
                
                            float? luftdruckStation = ConvertStringToFloatNull(sfeld[14]); // in hPa (hektoPascal)
                            int? sonneneinstrahlung = ConvertStringToIntNull(sfeld[15]); // in %

                            Wetterwert wwert = new Wetterwert
                            {
                                Messdatum = messdatum,
                                Temperatur = temperatur,
                                Taupunkt = taupunkt,
                                RelativeLuftfeuchtigkeit = relativeLF,
                                Windrichtung = windrichtung,
                                Windgeschwindigkeit = windgeschwindigkeit,
                                Windspitzenrichtung = windspitzenrichtung,
                                Windspitzengeschwindigkeit = windspitzengesch,
                                Niederschlag = niederschlag,
                                LuftdurckMeeresniveau = luftdruckMeeresniveau,
                                LuftdruckStationsniveau = luftdruckStation,
                                Sonneneinstrahlung = sonneneinstrahlung
                            };

                            Standort standort = new Standort { Ort = ortsname };

                            Wetterstation station = new Wetterstation
                            {
                                Wetterwert = wwert,
                                Standort = standort
                            };

                            this.list.Add(station);

                        }
               
                    }

        /// <summary>
        /// Wandelt einen String in eine ganze Zahl um. NULL-Wert möglich.
        /// von grudoa11
        /// </summary>
        /// <param name="s">umzuwandelte String</param>
        /// <returns>int?</returns>
        private int? ConvertStringToIntNull(string s)
        {
            try { return Convert.ToInt32(s); }
            catch { return null; }
        }
        /// <summary>
        /// Wandelt einen String in einen String um bei dem ein NULL-Wert möglich ist.
        /// von grudoa11
        /// </summary>
        /// <param name="s">umzuwandelte String</param>
        /// <returns>string?</returns>
        private string ConvertStringToStringNull(string s)
        {
            try { return s.ToString(); }
            catch { return ""; }
        }

        /// <summary>
        /// Wandelt einen String in eine float-Zahl um bei dem ein NULL-Wert möglich ist.
        /// von grudoa11
        /// </summary>
        /// <param name="s">umzuwandelte String</param>
        /// <returns>string?</returns>
        private float? ConvertStringToFloatNull(string s)
        {
            try { return Convert.ToSingle(s); }
            catch { return null; }
        }

        public String getChangeDate()
        {
            return m_DAL.getChangeDate();
        }

    }
}
