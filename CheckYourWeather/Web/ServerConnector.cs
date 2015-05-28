using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web
{

    /// <summary>
    /// @Autor: KOLLER Philipp
    /// 
    /// m_WebClient: ist für die Verbindung zu der Webseite nötig
    /// m_Url: verwaltet die URL, wo die Daten liegen
    /// m_Speicherort: Der Speicherort wird einmalig im Konstruktor zugewiesen.
    /// </summary>
    public class ServerConnector
    {

        private WebClient m_WebClient;
        private String m_Url;
        private String m_Speicherort;

        public String URL
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        public ServerConnector(String url)
        {
            m_Url = url;
            m_WebClient = new WebClient();
            m_Speicherort = AppDomain.CurrentDomain.BaseDirectory + "/Data/zamg.csv";
        }


        /// <summary>
        /// Die Methode saveCSV() speichert die Daten von der ZAMG auf den Server.
        /// </summary>
        /// <returns></returns>
        public string saveCSV()
        {
            try
            {
                
                m_WebClient.DownloadFile(m_Url, m_Speicherort);

                return m_Speicherort;
            } 
            catch(Exception exe)
            {
                return exe.Message;
            }
            
        }
        
    }
}
