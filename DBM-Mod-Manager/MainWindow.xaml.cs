using DBM_Mod_Manager.Actions;
using Octokit;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DBM_Mod_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GitHubClient _git = new GitHubClient(new ProductHeaderValue("DBM-Mods"));

        public class ModItem
        {
            public int Id { get; set; }
            public bool Enabled { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
            public string Section { get; set; }
            public string Dependencies { get; set; }

            public string Author { get; set; }
            public string Version { get; set; }
            public string LatestVersion { get; set; }
            public string DBMVersion { get; set; }
        }

        public ObservableCollection<ModItem> ModItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "DBM Mods - Mod Manager";
            this.CustomTitleBar.Text = this.Title;

            this.ModItems = new ObservableCollection<ModItem>();

            
            AddModsToList();

            lvMods.ItemsSource = ModItems;

        }

        private void AddModsToList()
        {
            var modParser = new ModParser();

            var path = @"D:\Steam\steamapps\common\Discord Bot Maker\actions";

            modParser.LoadAllScripts(path);

            var mods = modParser.JSMods;

            foreach (var item in mods)
            {
                ModItems.Add(new ModItem()
                {
                    Id = mods.IndexOf(item),
                    Enabled = true,
                    Name = item.Name,
                    Author = item.Author,
                    Section = item.Section,
                    FileName = item.FileName,
                    Dependencies = string.Join(",", item.Dependencies.ToArray())
                });

            }
        }

        #region test

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

        #endregion test

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ModItems.Clear();
            AddModsToList();
        }


    }
}