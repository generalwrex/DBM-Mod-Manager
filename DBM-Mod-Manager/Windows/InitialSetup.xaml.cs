using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using System.IO;


namespace DBM_Mod_Manager.Windows
{
    /// <summary>
    /// Interaction logic for SetupProcess.xaml
    /// </summary>
    public partial class InitialSetup : Window
    {

        public InitialSetup(Config config)
        {
            InitializeComponent();

        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(DBMPath.Text != "" && BotPath.Text != "")
            {
                Config.Instance.Settings.BotProjectPath = BotPath.Text;
                Config.Instance.Settings.DBMRootPath = DBMPath.Text;
            }

            Config.Instance.Settings.InitialSetupRan = true;

            Config.Instance.SaveConfiguration();

            Close();
        }

        private void DBMBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if(result == System.Windows.Forms.DialogResult.OK)
                {                  
                    DBMPath.Text = Config.Instance.Settings.DBMRootPath = dialog.SelectedPath;
                }
            }
        }

        private void BotBrowse_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    BotPath.Text = Config.Instance.Settings.BotProjectPath = dialog.SelectedPath;
                }
            }
        }
    }
}
