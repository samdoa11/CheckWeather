using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;

namespace Web.Classes
{
    /// <summary>
    /// Klasse für Dateioperationen
    /// </summary>
    ///
    public class DAL
    {
        private FileStream m_FileStream;
        private StreamReader m_StreamReader;
        private String m_FilePath;
        private static String DATAPATH = AppDomain.CurrentDomain.BaseDirectory + "Data\\";


        public DAL(String pfad)
        {
            //leseExcel(); => Das gehört nur dann aufgerufen wenn User auf Download Land Steiermark Dateien klickt
            this.m_FilePath = DATAPATH + pfad;
        }

        /// <summary>
        /// @author: Dominik Sammer
        /// Liest csv Dateien aus.
        /// Gibt die Zeile für Zeile zurück
        /// </summary>
        /// <returns>List mit den Zeilen der Datei</returns>
        public List<String> ReadOut()
        {
            this.m_FileStream = new FileStream(this.m_FilePath, FileMode.Open, FileAccess.ReadWrite);
            this.m_StreamReader = new StreamReader(this.m_FileStream);
            List<String> zeilen = new List<string>();
            String zeile = "";
            this.m_StreamReader.ReadLine();
            while ((zeile = this.m_StreamReader.ReadLine()) != null)
            {
                zeilen.Add(zeile);
            }
            this.m_StreamReader.Close();
            return zeilen;
        }
    
        /// <summary>
        /// Geht Filenamen durch und sortiert pro Wetterstation die Komponente um anschließend ein File für alle Wetterstationen zu erstellen
        /// </summary>
        /// <param name="datanames">Filenamen der Dateien</param>
        /// <returns>Dictonary mit String als Key (Wetterstationenid) und List<String> als Value mit den Komponentenids</returns>
        private Dictionary<String, List<String>> GetKomponentsPerWetterstation(String[] datanames)
        {
            Dictionary<String, List<String>> dict = new Dictionary<String, List<String>>();
            foreach (String pfad in datanames)
            {
                //Splitten
                string[] split = pfad.Split(new String[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);

                String id = split[split.Length - 1].Split('.')[0];


                //Kompontenten pro wetterstation
                char[] splChars = new char[] { '_' };
                String[] gesp = id.Split(splChars);

                if (gesp[1] != null)
                {
                    if (dict.Keys.Contains(gesp[1]))
                    {
                        dict[gesp[1]].Add(gesp[2]);
                    }
                    else
                    {
                        dict.Add(gesp[1], new List<string>() { gesp[2] });
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// @author: Dominic Groß
        /// Opens all Excel files von einer einzigen Wetterstation (mehrere Komponeten)
        /// </summary>
        /// <param name="pfad"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private String OpenTheExcelFile(List<String> dateipfaede, String id)
        {
            List<String[]> slist = new List<String[]>();
            String dateiinhalt = "";
            foreach (String pfad in dateipfaede)
            {
                //Variablen die Excel benötigt
                Excel._Application app = new Excel.Application(); //Die Excel-Applikation
                Excel.Workbook book = null; //Das Workbook
                Excel.Worksheet sheet = null; //Das Worksheet

                //Öffnen
                book = app.Workbooks.Open(pfad);

                //Wenn Excel im hintergrund nicht geöffnet werden soll
                app.Visible = false;
                app.ScreenUpdating = false;
                app.DisplayAlerts = false;

                //Worksheet auslesen, Keine zero-based index
                sheet = (Excel.Worksheet)book.Worksheets[1];

                //Spalten einen Namen zuweisen
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVQXYZ123456789,.";

                //Spalten zählen
                int colCount = sheet.UsedRange.Columns.Count;

                //Buchstabe der letzten Spalte
                char lastColChar = alphabet[colCount];

                //letzt genutze Spalte
                int rowCount = sheet.UsedRange.Rows.Count;

                //Range definieren
                Excel.Range range = sheet.UsedRange;

                //Excel Daten auslesen
                //List<String[]> slist = new List<String[]>();
                //String[,] sfeld = (String[,])range.Value;
                //slist.Add(sfeld);
                object[,] myExcelFileValues = range.Value2;
                //object[][] o = new object[myExcelFileValues.Length][];
                //myExcelFileValues.CopyTo(o,0);


                //Um File freizubgeben muss der Speicher gelöscht werden
                range = null;


                //Nun löschen wir das Worksheet und rufen den GarbageCollector auf:
                Marshal.FinalReleaseComObject(sheet);
                app.DisplayAlerts = false;
                sheet = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                //Anschließend lassen wir das Workbook schließen und löschen das Workbook-Objekt:
                book.Close(false);
                Marshal.FinalReleaseComObject(book);
                book = null;

                //Daraufhin schließen wir Excel/die Applikation und löschen diese aus dem Speicher:
                app.Quit();
                Marshal.FinalReleaseComObject(app);
                app = null;

                #region ausgabe
                //foreach (object s in myExcelFileValues)
                //{
                //    Console.WriteLine("" + s);
                //}
                #endregion



                for (int i = myExcelFileValues.GetLength(0); i > myExcelFileValues.GetLength(0) - 2; i--)
                {
                    String[] sfeld = new String[5];
                    for (int j = 1; j <= myExcelFileValues.GetLength(1); j++)
                    {
                        if (myExcelFileValues[i, j] != null)
                        {
                            if (j == 3)
                            {
                                sfeld[j - 1] = myExcelFileValues[i, j].ToString();
                                if (i == 5)
                                {
                                    sfeld[j] = "Station";
                                    sfeld[j + 1] = "Name";
                                }
                                else
                                {
                                    sfeld[j] = id + "";
                                    sfeld[j + 1] = myExcelFileValues[1, 1].ToString().Split(new String[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                }
                            }
                            else
                            {
                                sfeld[j - 1] = myExcelFileValues[i, j].ToString();
                            }
                        }


                    }
                    slist.Add(sfeld);

                }


            }

            String csvRow = "";
            for (int i = 0; i < slist.Count(); i++)
            {
                csvRow += string.Format("{0};{1};{2};{3};{4}", slist.ElementAt(i)[3], slist.ElementAt(i)[4], slist.ElementAt(i)[0], slist.ElementAt(i)[1], slist.ElementAt(i)[2]);
            }

            dateiinhalt += csvRow;

            return dateiinhalt;
        }


        /// <summary>
        /// 
        /// </summary>
        public void leseExcel()
        {


            #region ofd
            ////Mit OpenFileDialog eine Datei auswählen
            //OpenFileDialog Import = new OpenFileDialog();
            //Import.Filter = "Excel-Arbeitsmappe (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
            //if (Import.ShowDialog() == DialogResult.OK) { Pfad = Import.FileName; }
            #endregion
            String[] datanames = Directory.GetFiles(DATAPATH + "data_stmk\\");

            Dictionary<String, List<String>> komponentenProWetterstation = this.GetKomponentsPerWetterstation(datanames);
            Dictionary<String, List<String>> wetterstationenpfaddict = new Dictionary<string, List<String>>();
            Dictionary<String, List<String>> wetterstationenDateiinhaltdict = new Dictionary<string, List<string>>();

            //Finde für jede Wetterstation den Dateipfad und speichere in Dictonary
            foreach(KeyValuePair<String, List<String>> wetterstation in komponentenProWetterstation)
            {
                foreach(String komponente in wetterstation.Value)
                {
                    String dateipfad = DATAPATH + "data_stmk\\File_" + wetterstation.Key + "_" + komponente + ".xls";
                    if (wetterstationenpfaddict.Keys.Contains(wetterstation.Key))
                    {
                        wetterstationenpfaddict[wetterstation.Key + "-" + wetterstation.Value].Add(dateipfad);
                    }
                    else
                    {
                        wetterstationenpfaddict.Add(wetterstation.Key + "-" + komponente, new List<string>() { dateipfad });
                    }
                }
            }

            //Ein Dictonary mit allen Werten
            foreach (KeyValuePair<String, List<String>> wetterstationDateipfad in wetterstationenpfaddict)
            {
                    String id = wetterstationDateipfad.Key;

                    String slist = this.OpenTheExcelFile(wetterstationDateipfad.Value, id);

                    if (wetterstationenDateiinhaltdict.Keys.Contains(id))
                    {
                        wetterstationenDateiinhaltdict[id].Add(slist);
                    } else
                    {
                        wetterstationenDateiinhaltdict.Add(id, new List<string>() { slist });
                    }
            }

            //Abspeichern in eine CSV Datei

            ////Mit Stream-Writer alles in eine CSV Datei
            var file = DATAPATH + "\\landSteiermarkAlleStationen.csv";

            using (var stream = File.CreateText(file))
            {
                foreach (KeyValuePair<String, List<String>> wetterstationinhalt in wetterstationenDateiinhaltdict)
                {
                    foreach (String zeile in wetterstationinhalt.Value)
                    {
                        stream.WriteLine(zeile);
                    }
                }
            }



            Console.WriteLine("All Data Read");


        }

        public String GetLastChangeDateZamg()
        {
            string localFilename = this.m_FilePath;

            if(localFilename.Equals(""))
            {
                
                return "Keine Datei vorhanden";
            }
            else
            {
                FileInfo info = new FileInfo(localFilename);
                return "Letze aktualisierte Datei vom: " + info.LastWriteTime.ToString();
            }

        }
    }
}