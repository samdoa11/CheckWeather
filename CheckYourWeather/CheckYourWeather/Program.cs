using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckYourWeather
{
    public class Program
    {
        public const string TestUrl = "http://app.luis.steiermark.at/luft2/suche.php";

        private List<int> m_Ids;
        public Program()
        {
            
        }

        [STAThread]
        public static void Main(string[] args)
        {
            //Ids der Wetterstationen die es gibt
            LandSteiermark stmk = new LandSteiermark("station1");
            List<int> idList = stmk.getIds();

            //Ids der Komponenten die es gibt
            stmk = new LandSteiermark("komponente1");
            List<int> idListKomponente = stmk.getIds();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
            ServerConnectorLandSteiermark con = new ServerConnectorLandSteiermark();
            foreach (int id in idList)
            {
                String link = "http://app.luis.steiermark.at/luft2/export.php?station1="+id +
                    "&station2&komponente1=8&station3=&station4=&komponente2=&von_tag=" + DateTime.Now.Day+
                    "&von_monat=" + DateTime.Now.Month + "&von_jahr=" + DateTime.Now.Year +
                    "&mittelwert=1&bis_tag=" + DateTime.Now.Day + "&bis_monat=" + DateTime.Now.Month +
                    "&bis_jahr=" + DateTime.Now.Year;
                
                con.saveCSV(link, id);
            }


        }
    }

    public class LandSteiermark {
        //Members
        public const string TestUrl = "http://app.luis.steiermark.at/luft2/suche.php";
        private List<int> m_Ids;
        private String id;

        public LandSteiermark(String id)
        {
            this.id = id;
        }

        /// <summary>
        /// Author: Dominik Sammer
        /// Methode die eine Liste der gesamten Ids der Wetterstationen zurückgibt.
        /// Die benötigt man später zum zusammenbauen des Links für den Download.
        /// Die Aufrufende Methode muss ein STATThreat sein!
        /// @Return: Liste von Ints, bei Fehler wird eine Leere Liste zurückgegeben.
        /// </summary>

        public List<int> getIds()
        {
            WebBrowser wb = new WebBrowser();
            Program p = new Program();
            try
            {
                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
                wb.Navigate(TestUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new List<int>();
            }

            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }


            return this.m_Ids;
        }


        public void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                WebBrowser wb = (WebBrowser)sender;

                HtmlElement document = wb.Document.GetElementsByTagName("html")[0];

                HtmlElement element = wb.Document.GetElementById(this.id);

                List<int> idList = new List<int>();
                String s1 = element.InnerHtml;
                String[] split = s1.Split(' ');
                foreach (String teil in split)
                {
                    if (teil.Contains("value"))
                    {
                        String value = teil.Split('=')[1];
                        value = value.Split('>')[0];
                        if (value != "\"\"")
                        {
                            idList.Add(Convert.ToInt32(value));
                        }
                    }
                }
                this.m_Ids = idList;
            }
            catch (Exception ex)
            {
               
            }
        }
    }


}
