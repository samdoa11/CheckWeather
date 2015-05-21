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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.m_liste = new Wetterstationenliste();

            this.createOutputZAMG();
        }

        /// <summary>
        /// Methode erstellt den Output der ZAMG Daten
        /// </summary>
        public void createOutputZAMG()
        {
            String output = "<table class='table'>";
            foreach (Wetterstation wert in this.m_liste)
            {
                output += wert.ToString();
            }
            labAusgabe.Text = output;
        }
    }
}