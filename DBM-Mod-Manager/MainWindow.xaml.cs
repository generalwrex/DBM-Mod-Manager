using DBM_Mod_Manager.Actions;
using Octokit;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.JScript;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using DBM_Mod_Manager.Models;
using System.Linq;
using DBM_Mod_Manager.Managers;
using DBM_Mod_Manager.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace DBM_Mod_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GitHubClient _git = new GitHubClient(new ProductHeaderValue("DBM-Mods"));

        DownloadManager spwindow;

        public ObservableCollection<ModItem> ModItems { get; set; }
        public ObservableCollection<ModItem> ModItems2 { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Config.AppPath = Path.Combine(Environment.CurrentDirectory, "data");

            Config config = new Config();

            if (!config.Settings.InitialSetupRan || config.Settings.BotProjectPath == "" || config.Settings.DBMRootPath == "")
            {
                var init = new InitialSetup(config);
                init.ShowDialog();
            }


            spwindow = new DownloadManager();


            this.Title = "DBM Mods - Mod Manager";
            this.CustomTitleBar.Text = this.Title;

            this.ModItems = new ObservableCollection<ModItem>();

            lvMods.ItemsSource = AddModsToList(@"D:\Steam\steamapps\common\Discord Bot Maker\actions").OrderBy(x => x.Section);


            lvMods2.ItemsSource = AddModsToList(@"F:\Wrex\Desktop\DBM-Mods\actions").OrderBy(x => x.Section);

            
        }

        private ObservableCollection<ModItem> AddModsToList(string path)
        {
            var templist = new ObservableCollection<ModItem>();

            var modParser = new ModParser();

            modParser.LoadAllScripts(path);

            var mods = modParser.JSMods;

            try
            {
                foreach (var item in mods)
                {
                    templist.Add(new ModItem()
                    {
                        Id = mods.IndexOf(item),
                        Enabled = true,
                        Name = item.Name,
                        CurrentVersion = string.IsNullOrEmpty(item.Version) ? "1.0.0" : item.Version,
                        LatestVersion = string.IsNullOrEmpty(item.Version) ? "1.0.0" : item.Version,
                        Author = item.Author,
                        Section = item.Section,
                        Description = item.Description,
                        FileName = item.FileName,
                        DependsOn = item.DependsOnMods == null ? "" : string.Join(",", item.DependsOnMods?.ToArray()),
                        Dependencies = item.Dependencies == null ? "" : string.Join(",", item.Dependencies?.ToArray())
                    });

                }

                return templist;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return new ObservableCollection<ModItem>();
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
            lvMods.ItemsSource = AddModsToList(@"D:\Steam\steamapps\common\Discord Bot Maker\actions").OrderBy(x => x.Section);
        }


        private void Section_Sort_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lvMods.ItemsSource = new ObservableCollection<ModItem>(ModItems.OrderBy(s => s.Section));
        }

        private void Name_Sort_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lvMods.ItemsSource = new ObservableCollection<ModItem>(ModItems.OrderBy(s => s.Name));
        }

        private void LV_Sort_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lvMods.ItemsSource = new ObservableCollection<ModItem>(ModItems.OrderBy(s => s.LatestVersion));
        }

        private void CV_Sort_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lvMods.ItemsSource = new ObservableCollection<ModItem>(ModItems.OrderBy(s => s.CurrentVersion));
        }

        private void Author_Sort_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lvMods.ItemsSource = new ObservableCollection<ModItem>(ModItems.OrderBy(s => s.Author));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {          
            spwindow.DownloadLatestRelease();
           
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(spwindow != null)
                spwindow.Close();
        }
    }
}