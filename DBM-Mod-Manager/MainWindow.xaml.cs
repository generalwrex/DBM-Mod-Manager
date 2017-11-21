using DBM_Mod_Manager.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DBM_Mod_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class ModItem
        {
            public string Name { get; set; }
            public string Author { get; set; }
            public string Version { get; set; }
            public string LatestVersion { get; set; }
            public string DBMVersion { get; set; }
            public string Dependencies { get; set; }
        }

        public ObservableCollection<ModItem> ModItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "DBM Mods - Mod Manager";
            this.CustomTitleBar.Text = this.Title;

            this.ModItems = new ObservableCollection<ModItem>();

            ModItems.Add(new ModItem()
            {
                Name = "My Mod",
                Author = "generalwrex",
                Version = "1.0.0",
                LatestVersion = "1.0.0",
                DBMVersion = "Latest",
                Dependencies = "node.js"
            });

            ModItems.Add(new ModItem()
            {
                Name = "My Mod 2",
                Author = "generalwrex",
                Version = "1.0.0",
                LatestVersion = "1.0.0",
                DBMVersion = "Latest",
                Dependencies = "node.js"
            });

            lvMods.ItemsSource = ModItems;
        }

        private void lvModsColumnHeader_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //Content="&#x1f5d9;" < Windows Restore HEX Code
            window.Close();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            //Content="&#x1f5d6;" < Windows Restore HEX Code
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
                MaxButton.Content = System.Net.WebUtility.HtmlDecode("&#x1f5d7;");
            }
            else
            {
                window.WindowState = WindowState.Normal;
                MaxButton.Content = System.Net.WebUtility.HtmlDecode("&#x1f5d6;");
            }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            window.WindowState = WindowState.Minimized;
        }
    }
}