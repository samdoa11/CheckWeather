using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace Web
{

    /// <summary>
    /// @Philipp
    /// </summary>
    public class ServerConnectorLandSteiermark
    {
        private WebClient m_WebClient;
        private LandSteiermarkPage m_LandSteiermarkPage;

        public ServerConnectorLandSteiermark()
        {
            m_WebClient = new WebClient();
            this.m_LandSteiermarkPage = new LandSteiermarkPage("station1");
        }


        /// <summary>
        /// Die Methode saveCSV() speichert die Daten vom Land Steiermark
        /// auf den Server.
        /// <param name="websitePath">Url zur Webseite</param>"
        /// <param name="id">Id der Wetterstation => späterer Dateiname</param>
        /// </summary>
        private void saveExcel(String websitePath, int id)
        {
            string remoteFilename = websitePath;
            string localFilename = AppDomain.CurrentDomain.BaseDirectory + "Data\\data_stmk\\"+ id+".xls";

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(remoteFilename, localFilename);

                //Wenn Fehler dann ist Datei < 4 kb => 4096 byte
                FileInfo info = new FileInfo(localFilename);
                if (info.Length < 4096)
                {
                    File.Delete(localFilename);
                }
            }
        }

        /// <summary>
        /// Methode ist die Schnittstelle zum Download der Dateien des Landes Steiermark
        /// Hier werden zuerst die Urls zusammengebaut und anschließend
        /// wird der Download gestartet und die Datei gespeichert.
        /// </summary>
        public void DownloadDateien()
        {
            //Ids der Wetterstationen die es gibt
            
            List<int> idList = this.m_LandSteiermarkPage.GetIds();

            //Ids der Komponenten die es gibt
            this.m_LandSteiermarkPage.ElementId = "komponente1";
            List<int> idListKomponente = this.m_LandSteiermarkPage.GetIds();
           
            foreach (int id in idList)
            {
                String link = "http://app.luis.steiermark.at/luft2/export.php?station1=" + id +
                    "&station2&komponente1=8&station3=&station4=&komponente2=&von_tag=" + DateTime.Now.Day +
                    "&von_monat=" + DateTime.Now.Month + "&von_jahr=" + DateTime.Now.Year +
                    "&mittelwert=1&bis_tag=" + DateTime.Now.Day + "&bis_monat=" + DateTime.Now.Month +
                    "&bis_jahr=" + DateTime.Now.Year;
              
                this.saveExcel(link, id);

            }
        }

    }

       public class LandSteiermarkPage {
        //Members
        public const string URL = "http://app.luis.steiermark.at/luft2/suche.php";
        private List<int> m_Ids;
        public String ElementId { get; set; }

        public LandSteiermarkPage(String id)
        {
            this.ElementId = id;
        }

        /// <summary>
        /// Author: Dominik Sammer
        /// Methode die eine Liste der gesamten Ids der Wetterstationen zurückgibt.
        /// Die benötigt man später zum zusammenbauen des Links für den Download.
        /// Die Aufrufende Methode muss ein STATThreat sein!
        /// @Return: Liste von Ints, bei Fehler wird eine Leere Liste zurückgegeben.
        /// </summary>

        public List<int> GetIds()
        {
            WebBrowser wb = new WebBrowser();
            try
            {
                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
                wb.Navigate(URL);
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

                HtmlElement element = wb.Document.GetElementById(this.ElementId);

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
