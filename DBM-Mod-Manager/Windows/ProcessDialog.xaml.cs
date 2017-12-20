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
    public partial class ProcessDialog : Window
    {

        public ProcessDialog()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            Topmost = true;
            Visibility = Visibility.Hidden;
           
        }


        public void ChangeInfoText(string text)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => ChangeInfoText(text));
                return;
            }
            InfoBox.Text = text;
        }

        public void ChangeProgressText(string text)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => ChangeProgressText(text));
                return;
            }
            ProgressInfo.Text = text;
        }

        public void ChangeProgressBarValue(int value)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => ChangeProgressBarValue(value));
                return;
            }
            ProgressBar.Value = value;
        }

        public void ChangeProgressBarMaxValue(int value)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => ChangeProgressBarMaxValue(value));
                return;
            }
            ProgressBar.Maximum = value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
