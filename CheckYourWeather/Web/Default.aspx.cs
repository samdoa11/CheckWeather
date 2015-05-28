using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Classes;

namespace Web
{
    /// <summary>
    /// Class that handles events from the aspx site
    /// 
    /// </summary>
    public partial class _Default : Page
    {
        private Wetterstationenliste m_liste;
        private ServerConnector m_ZamgServer;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void onDownloadFile(object sender, EventArgs e)
        {
<<<<<<< HEAD
            
            // @Autor: Lisa Schwarz -> Aufruf de ServerConnection Klasse + weitergabe des Links
            this.m_ZamgServer = new ServerConnector("http://www.zamg.ac.at/ogd/");
            String pfad = this.m_ZamgServer.saveCSV();
            
            this.m_liste = new Wetterstationenliste(pfad);

            //Session speichert die aktuellen Variablen
            Session.Add("Wetterwerte", this.m_liste);
        }

        protected void onShowFile(object sender, EventArgs e)
        {
            this.m_liste = (Wetterstationenliste)Session["Wetterwerte"];
            if (this.m_liste == null) labAusgabe.Text = "Please download the file!";
            else
                this.createOutputZAMG();
=======

            this.m_liste = new Wetterstationenliste();

            

            this.createOutputZAMG();

            labChangeDate.Text = this.m_liste.getChangeDate();
>>>>>>> origin/master
        }

        /// <summary>
        /// Methode erstellt den Output der ZAMG Daten
        /// </summary>
        public void createOutputZAMG()
        {
            String output = "<table class='table'>";
            output += "<tr><th>Stationsname</th>"
                + "<th>Datum</th>"
                + "<th>Temperatur (°C)</th>"
                + "<th>Taupunkt (°C)</th>"
                + "<th>Relative Luftfeuchtigkeit (%)</th>"
                + "<th>Windrichtung (°)</th>"
                + "<th>Windgeschwindigkeit (km/h)</th>"
                + "<th>Windspitzenrichtung (°)</th>"
                + "<th>Windspitzengeschwindigkeit (km/h)</th>"
                + "<th>Lufthruck Meeresniveau (hPa)</th>"
                + "<th>Lufthruck Stationsniveau (hPa)</th>"
                + "<th>Niederschlag (l/m²)</th>"
                + "<th>Sonneneinstrahlung (%)</th></tr>";
            foreach (Wetterstation wert in this.m_liste)
            {
                output += wert.ToString();
            }
            labAusgabe.Text = output;
        }
    }
}