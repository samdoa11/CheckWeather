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
    /// Klasse für das Auslesen einer Datei
    /// Author Dominik Sammer
    /// </summary>
    ///
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
            leseExcel();
<<<<<<< HEAD


=======
>>>>>>> origin/master
        }

        //Returns List of Strings from the filecontent
        public List<String> readOut()
        {
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

        public void leseExcel()
        {


            #region ofd
            ////Mit OpenFileDialog eine Datei auswählen
            //OpenFileDialog Import = new OpenFileDialog();
            //Import.Filter = "Excel-Arbeitsmappe (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
            //if (Import.ShowDialog() == DialogResult.OK) { Pfad = Import.FileName; }
            #endregion


<<<<<<< HEAD
            String[] datanames = Directory.GetFiles(@"" + AppDomain.CurrentDomain + "CheckYourWeather\\Data\\data_stmk\\");
=======
            String[] datanames = Directory.GetFiles(@"" + AppDomain.CurrentDomain.BaseDirectory + "Data\\data_stmk\\");
>>>>>>> origin/master

            foreach (string pfad in datanames)
            {
                //Splitten
                string[] split = pfad.Split(new String[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);

                string id = split[split.Length - 1].Split('.')[0];


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

                List<String[]> slist = new List<String[]>();


                for (int i = 5; i <= myExcelFileValues.GetLength(0); i++)
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

                        Console.Write(myExcelFileValues[i, j] + "\t");

                    }
                    slist.Add(sfeld);
                    Console.WriteLine();

                }
                int anz = slist.Count;

                //Mit Stream-Writer alles in eine CSV Datei
<<<<<<< HEAD
                var file = AppDomain.CurrentDomain + "CheckYourWeather\\Data\\csv_stmk\\" + id + ".csv";
=======
                var file = AppDomain.CurrentDomain.BaseDirectory + "Data\\csv_stmk\\" + id + ".csv";
>>>>>>> origin/master

                using (var stream = File.CreateText(file))
                {

                    for (int i = 0; i < slist.Count(); i++)
                    {

                        string csvRow = string.Format("{0};{1};;{2};{3};{4};;;;;;;;;;", slist.ElementAt(i)[3], slist.ElementAt(i)[4], slist.ElementAt(i)[0], slist.ElementAt(i)[1], slist.ElementAt(i)[2]);

                        stream.WriteLine(csvRow);
                    }
                }
            }



            Console.WriteLine("All Data Read");


        }

        public String getChangeDate()
        {
            string localFilename = m_FilePath;

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