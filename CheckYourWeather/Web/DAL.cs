using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Classes
{
    /// <summary>
    /// Klasse für das Auslesen einer Datei
    /// Author Dominik Sammer
    /// </summary>
    public class DAL
    {
        private FileStream m_FileStream;
        private StreamReader m_StreamReader;
        private String m_FilePath;

        public DAL(String pfad)
        {
            this.m_FilePath = pfad;
            this.m_FileStream = new FileStream(this.m_FilePath, FileMode.Open, FileAccess.ReadWrite);
            this.m_StreamReader = new StreamReader(this.m_FileStream);
        }

        //Returns List of Strings from the filecontent
        public List<String> readOut()
        {
            List<String> zeilen = new List<string>();
            String zeile = "";
            this.m_StreamReader.ReadLine();
            while ((zeile = this.m_StreamReader.ReadLine()) != null)
            {
                zeilen.Add(zeile);
            }
            return zeilen;
        }
    }
}