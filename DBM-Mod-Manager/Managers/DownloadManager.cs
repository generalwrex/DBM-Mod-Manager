using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBM_Mod_Manager.Windows;

using System.Net;

namespace DBM_Mod_Manager.Managers
{
    public class DownloadManager : ProcessDialog
    {
        public DownloadManager()
        {           
            InfoBox.Text = "Downloading the latest list of mods! \r\n\r\n  This may take a few minutes!";

        }

        public bool DownloadLatestRelease()
        {
            Show();

            try
            {
                Console.WriteLine("HellionExtendedServer:  Downloading latest release...");

                WebClient client = new WebClient();
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(ReleaseDownloaded);

                client.DownloadFileAsync(new Uri(@"https://github.com/Discord-Bot-Maker-Mods/DBM-Mods/archive/master.zip"), "test.zip");

                client.DownloadProgressChanged += Client_DownloadProgressChanged;


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("HellionExtendedServer:  Update Failed (DownloadLatestRelease)" + ex.ToString());
            }
            return false;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ChangeProgressBarMaxValue((int)e.TotalBytesToReceive);
            ChangeProgressBarValue((int)e.BytesReceived);
            ChangeProgressText("Downloaded: " + (e.BytesReceived / 1000000) + " MB / " + (e.TotalBytesToReceive / 1000000) + " MB");
        }

        private void ReleaseDownloaded(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("HellionExtendedServer:  Update Failed (ReleaseDownloadedEvent)" + ex.ToString());
            }
        }
    }
}
