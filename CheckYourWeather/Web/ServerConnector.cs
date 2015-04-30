﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Serverbindung
{

    /// <summary>
    /// @Autor: KOLLER Philipp
    /// 
    /// m_WebClient: ist für die Verbindung zu der Webseite nötig
    /// m_Url: verwaltet die URL, wo die Daten liegen
    /// </summary>
    public class ServerConnector
    {

        private WebClient m_WebClient;
        private String m_Url;
        

        public String URL
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        public ServerConnector(String url)
        {
            m_Url = url;
            m_WebClient = new WebClient();
        }

        /// <summary>
        /// Die Methode saveCSV() speichert die Daten von der ZAMG auf den Server.
        /// </summary>
        /// <returns></returns>
        public string saveCSV()
        {
            try
            {
                WebClient m_WebClient = new WebClient();
                String speicherort = @"F:\Schule\4AHIF\FHKärnten\Projekt\CheckYourWeather\Serververbindung\Serverbindung\Test\test.csv";
                m_WebClient.DownloadFile(m_Url, speicherort);

                return speicherort;
            } 
            catch(Exception exe)
            {
                return exe.Message;
            }
            
        }
        
    }
}