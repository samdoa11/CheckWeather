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
                            str.Trim('"');
                            String[] sfeld = str.Split(';');
                            // Station
                            int stationsnummer = Convert.ToInt32(sfeld[0]);
                            string ortsname = sfeld[1];
                            int seehoehe = Convert.ToInt32(sfeld[2]); // in Meter

                            // Wetterdaten (nach reihenfolge in csv-datei)
                            //DateTime messdatum = Convert.ToDateTime(sfeld[3]); // Datum und Zeit der Messung
                            DateTime messdatum = new DateTime();
                            float temperatur = Convert.ToSingle(sfeld[5]); // in °C
                            float taupunkt = Convert.ToSingle(sfeld[6]); // in °C
                            int relativeLF = Convert.ToInt32(sfeld[7]); // in %

                            int windrichtung = 0;
                            if (sfeld[8] != "")
                            {
                                windrichtung = Convert.ToInt32(sfeld[8]); // in °
                            }
                            
                            float windgeschwindigkeit = Convert.ToSingle(sfeld[9]); // in km/h
                            // Windspitze kann auch NULL sein
                            int? windspitzenrichtung = 0;// in °
                            if (sfeld[10] != "")
                            {
                                windspitzenrichtung = Convert.ToInt32(sfeld[10]); // in °
                            }
                            
                            float windspitzengesch = Convert.ToSingle(sfeld[11]); // in km/h
                            // NULL-Wert möglich
                  float? niederschlag  = 0;
                            if (sfeld[12] != "") 
                              niederschlag = Convert.ToSingle(sfeld[12]); // in l/m²

                            float? luftdruckMeeresniveau = 0;
                            if (sfeld[13] != "") 
                    Convert.ToSingle(sfeld[13]); // in hPa (hektoPascal)
                
                            float luftdruckStation = Convert.ToSingle(sfeld[14]); // in hPa (hektoPascal)
                            int sonneneinstrahlung = Convert.ToInt32(sfeld[15]); // in %

                            Wetterwert wwert = new Wetterwert
                            {
                                Datum = messdatum,
                                Temperatur = temperatur,
                                Taupunkt = taupunkt,
                                Luftfeuchtigkeit = relativeLF,
                                Windrichtung = windrichtung,
                                Windgeschwindigkeit = windgeschwindigkeit,
                                Windspitze = windspitzenrichtung,
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
                }
        }
