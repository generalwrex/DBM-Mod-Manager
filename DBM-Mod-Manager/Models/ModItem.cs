using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBM_Mod_Manager.Models
{
    public class ModItem 
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }
        public string DependsOn { get; set; }
        public string Dependencies { get; set; }

        public string Author { get; set; }
        public string CurrentVersion { get; set; }
        public string LatestVersion { get; set; }
        public string DBMVersion { get; set; }

    }
}
