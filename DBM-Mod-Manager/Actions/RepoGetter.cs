using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

using System.IO;
using System.Windows;


namespace DBM_Mod_Manager.Actions
{
    public class DBMGitHub
    {
        private GitHubClient _github;

        private const string _DEFUSER = "Discord-Bot-Maker-Mods";
        private const string _DEFREPO = "DBM-Mods";
        private const string _DEFBRANCH = "master";

        private const string _CLONEURL = @"https://github.com/Discord-Bot-Maker-Mods/DBM-Mods.git";


        public string User { get; private set; }
        public string Repo { get; private set; }

        public string CloneDirectory;

        public string DefaultBranch { get; private set; }
        public string BetaBranch { get; private set; }

        public Octokit.Repository DBMRepo = new Octokit.Repository();


        public DBMGitHub(string user = _DEFUSER, string repo = _DEFREPO)
        {
            User = user;
            Repo = repo;

            CloneDirectory = Path.Combine(Environment.CurrentDirectory, "data", "clone", "default");

            _github = new GitHubClient(new ProductHeaderValue("DBM-Mods"));

            GetRepository();
        }


        public Octokit.Repository GetRepository()
        {
           return DBMRepo =  _github.Repository.Get(User, Repo).GetAwaiter().GetResult();
        }


        public List<string> DownloadFileList()
        {
            var tmp = new List<string>();

            var fileList = _github.Repository.Content.GetAllContentsByRef(User, Repo, "actions", "master").ConfigureAwait(false).GetAwaiter().GetResult();

            foreach(var file in fileList)
            {
                tmp.Add(file.Name);
            }

            return tmp;
        }

    }

}
