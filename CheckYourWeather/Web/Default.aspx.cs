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
        private Wetterstationenliste m_liste;


        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_liste = new Wetterstationenliste();

            this.createOutput();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //@Autor Lisa Schwarz
            Wetterstationenliste w = new Wetterstationenliste();

            //labAusgabe.Text = "adsf";
            //WetterList we = new WetterList();

        }

        public void createOutput()
        {
            foreach (Wetterstation wert in this.m_liste)
            {
                labAusgabe.Text += wert.ToString();
            }
        }
    }
}