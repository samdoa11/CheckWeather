using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace CheckYourWeather
{

    /// <summary>
    /// @Philipp
    /// </summary>
    public class ServerConnectorLandSteiermark
    {
        private WebClient m_WebClient;


        public ServerConnectorLandSteiermark()
        {
            m_WebClient = new WebClient();
        }


        /// <summary>
        /// Die Methode saveCSV() speichert die Daten von der ZAMG auf den Server.
        /// </summary>
        /// <returns></returns>
        public void saveCSV(String websitePath, int id)
        {
            string remoteFilename = websitePath;
            string localFilename = AppDomain.CurrentDomain.BaseDirectory +"data_stmk\\"+ id+".xls";

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

        //    Stream remoteStream = null;
        //    Stream localStream = null;
        //    WebRequest request = WebRequest.Create(remoteFilename);
        //    WebResponse response = request.GetResponse();
        //    // Create the local file
        //    using (localStream = File.Create(localFilename))
        //    {
        //        // Allocate a 1k buffer
        //        byte[] buffer = new byte[1024];
        //        int bytesRead;
        //        remoteStream = response.GetResponseStream();

        //        // Simple do/while loop to read from stream until
        //        // no bytes are returned
        //        do
        //        {
        //            // Read data (up to 1k) from the stream
        //            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

        //            // Write the data to the local file
        //            localStream.Write(buffer, 0, bytesRead);

        //            // Increment total bytes processed
        //        } while (bytesRead > 0);

        //        localStream.Flush();
        //    }
        //    //try
        //    //{
        //    //     Create a request for the specified remote file name
        //    //    WebRequest request = WebRequest.Create(remoteFilename);
        //    //    if (request != null)
        //    //    {
                   
        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    MessageBox.Show(e.Message);
        //    //}
        //    //finally
        //    //{
        //    //     Close the response and streams objects here 
        //    //     to make sure they're closed even if an exception
        //    //     is thrown at some point
        //    //    if (response != null) response.Close();
        //    //    if (remoteStream != null) remoteStream.Close();
        //    //    if (localStream != null) localStream.Close();
        //    //}




        //}


        ////            }



        ////            return "";
        ////        }
        ////        catch (Exception exe)
        ////        {
        ////            return exe.Message;
        ////        }

        ////    }
        }

    }
}