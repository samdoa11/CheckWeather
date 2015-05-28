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
    /// </summary>
    public partial class _Default : Page
    {
        private Wetterstationenliste m_liste;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_liste = new Wetterstationenliste();
            labChangeDate.Text = this.m_liste.getChangeDate();
            
            Session.Add("Wetterwerte", this.m_liste);
        }


        protected void onDownloadFile(object sender, EventArgs e)
        {
            this.m_liste = (Wetterstationenliste)Session["Wetterwerte"];
            if (this.m_liste == null)
            {

            }
            else
            {
                // @Autor: Lisa Schwarz -> Aufruf de ServerConnection Klasse + weitergabe des Links
                this.m_liste.DownloadZamgFile();

                //Session speichert die aktuellen Variablen
                Session.Add("Wetterwerte", this.m_liste);
            }
        }

        protected void onShowFile(object sender, EventArgs e)
        {
            //this.m_liste = (Wetterstationenliste)Session["Wetterwerte"]; => Nicht notwendig, denn wenn es gerade Datei gibt muss man sie nicht nochmal runter laden
            this.m_liste = new Wetterstationenliste();
            if (this.m_liste.getChangeDate() == "Keine Datei vorhanden") labAusgabe.Text = "Please download the file!";
            else
            {
                this.m_liste.ConvertCSV();
                this.createOutputZAMG();
            }
        }


        protected void OnGetDataFromLandSteiermark(object sender, EventArgs e)
        {
            this.m_liste = new Wetterstationenliste();
            this.m_liste.GetDataFromStmk();
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