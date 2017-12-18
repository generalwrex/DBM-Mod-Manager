using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace DBM_Mod_Manager.Actions
{
    public class ModParser
    {
        public class JSMod
        {
            public string FileName;
            public string Name;
            public string Version;
            public string Author;
            public string Section;
            public string Description;

            public List<string> Dependencies;
            public List<string> DependsOnMods;

            public JSMod()
            {
                FileName = "";
                Name = "";
                Section = "";
                Author = "";
                Version = "";
                Description = "";
                Dependencies = null;
                DependsOnMods = null;
            }

        }

        public List<JSMod> JSMods = new List<JSMod>();

        public JSMod DefaultMod = new JSMod();

        private string ParseStringVar(string name, string input)
        {
            string pattern = @"^(?:.*" + name + @")(?:.*[""|'])(.*)(?:""|')(?:,|;)";

            Match match = Regex.Match(input, pattern);
            if (match.Success && match.Groups.Count > 1)
                return match.Groups[1]?.Value;

            return null;
        }

        private List<string> ParseArrayVar(string name, string input)
        {
            List<string> list = new List<string>();

            string pattern = @"^(?:.*" + name + @")(?:.*)\[(.*)\](?:,|;)";

            Match match = Regex.Match(input, pattern);
            if (match.Success && match.Groups.Count > 1)
            {
                string value = match.Groups[1]?.Value;
                if (!String.IsNullOrEmpty(value) && value?.Length > 0)
                {
                    var deps = value?.Split(',');
                    if (deps?.Length > 0)
                        foreach (string dep in deps)
                            if (!String.IsNullOrEmpty(dep))
                                list.Add(dep.Trim('"', '\'').Replace("\"", "").Replace("\'",""));

                    return list;
                }
            }
            return null;
        }


        public void LoadAllScripts(string path)
        {
            foreach (var file in Directory.GetFiles(path, "*_MOD.js", SearchOption.TopDirectoryOnly))
            {
                JSMod script = LoadScript(file);

                if (script != null)
                {
                    JSMods.Add(script);
                }
            }
        }

        public JSMod LoadScript(string file)
        {
            try
            {
                using (StreamReader r = new StreamReader(file))
                {
                    JSMod mod = new JSMod();

                    mod.FileName = Path.GetFileNameWithoutExtension(file);

                    while (r.Peek() >= 0)
                    {
                        var line = Regex.Unescape(r.ReadLine());

                        if (String.IsNullOrEmpty(mod.Name))
                            mod.Name = ParseStringVar("name", line);

                        if (String.IsNullOrEmpty(mod.Section))
                            mod.Section = ParseStringVar("section", line);

                        if (String.IsNullOrEmpty(mod.Author))
                            mod.Author = ParseStringVar("author", line);

                        if (String.IsNullOrEmpty(mod.Description))
                            mod.Description = ParseStringVar("short_description", line);

                        if (String.IsNullOrEmpty(mod.Version))
                            mod.Version = ParseStringVar("version", line);

                        if (mod.Dependencies == null)
                            mod.Dependencies = ParseArrayVar("dependencies", line);

                        if (mod.DependsOnMods == null)
                            mod.DependsOnMods = ParseArrayVar("depends_on_mods", line);
                    }

                    return mod;                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }
    }
}