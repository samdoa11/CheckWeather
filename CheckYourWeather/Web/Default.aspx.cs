using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Classes;

namespace Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //@Autor Lisa Schwarz
            Wetterstationenliste w = new Wetterstationenliste();
            foreach(Wetterstation wert in w)
            {
                labAusgabe.Text = "Stationsstandort: " + wert.m_Standort + ", Stations Nummer: " + wert.m_Stationsnummer 
                    + "\nWerte:\n" + wert.m_Wetterwert.ToString();
            }
            //labAusgabe.Text = "adsf";
            //WetterList we = new WetterList();

        }
    }
}