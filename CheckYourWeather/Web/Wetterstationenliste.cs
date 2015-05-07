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
        private List<Wetterstation> list;
        private DAL m_DAL;
        private ServerConnector m_ServerCon;

        public Wetterstationenliste()
        {
            list = new List<Wetterstation>();

            // @Autor: Lisa Schwarz -> Aufruf de ServerConnection Klasse + weitergabe des Links
            m_ServerCon = new ServerConnector(@"http:\\www.zamg.ac.at\ogd\");
            m_ServerCon.saveCSV();
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
            List<string> input = m_DAL.readOut();
            foreach(string str in input)
                Console.WriteLine(str);

        }
    }
}