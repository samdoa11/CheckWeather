using Serverbindung;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Classes;

namespace Web
{
    public class Wetterstationenliste:IList<Wetterstation>
    {
        private List<Wetterstation> m_Wetterstationen;
        private DAL m_DAL;
        private ServerConnector m_ServerCon;

        public Wetterstationenliste()
        {
<<<<<<< HEAD
            m_Wetterstationen = new List<Wetterstation>();
=======
            list = new List<Wetterstation>();


            m_ServerCon = new ServerConnector(@"http:\\www.zamg.ac.at\ogd\");
            m_ServerCon.saveCSV();
>>>>>>> origin/master
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
                return m_Wetterstationen[index];
            }
            set
            {
                m_Wetterstationen[index] = value;
            }
        }

        public void Add(Wetterstation item)
        {
            m_Wetterstationen.Add(item);
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
            get { return m_Wetterstationen.Count; }
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
            return m_Wetterstationen.GetEnumerator();
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
            List<string> input = m_DAL.readOut();
            foreach (string str in input)
            {
                str.Trim('"');
                String[] sfeld = str.Split(';');
                // Station
                int stationsnummer = Convert.ToInt32(sfeld[0]);
                string ortsname = sfeld[1];
                int seehoehe = Convert.ToInt32(sfeld[2]); // in Meter

                // Wetterdaten (nach reihenfolge in csv-datei)
                DateTime messdatum = Convert.ToDateTime(sfeld[3]); // Datum und Zeit der Messung
                float temperatur = Convert.ToSingle(sfeld[4]); // in °C
                float taupunkt = Convert.ToSingle(sfeld[5]); // in °C
                int relativeLF = Convert.ToInt32(sfeld[6]); // in %
                int windrichtung = Convert.ToInt32(sfeld[7]); // in °
                float windgeschwindigkeit = Convert.ToSingle(sfeld[8]); // in km/h
                // Windspitze kann auch NULL sein
                int? windspitzenrichtung = Convert.ToInt32(sfeld[9]); // in °
                float windspitzengesch = Convert.ToInt32(sfeld[10]); // in km/h
                // NULL-Wert möglich
                float? niederschlag = Convert.ToSingle(sfeld[11]); // in l/m²
                float? luftdruckMeeresniveau = Convert.ToSingle(sfeld[12]); // in hPa (hektoPascal)
                
                float luftdruckStation = Convert.ToSingle(sfeld[13]); // in hPa (hektoPascal)
                int sonneneinstrahlung = Convert.ToInt32(sfeld[14]); // in %

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
                    m_Stationsnummer = stationsnummer,
                    m_Wetterwert = wwert,
                    m_Standort = standort
                };

                m_Wetterstationen.Add(station);

            }
               
        }
    }
}