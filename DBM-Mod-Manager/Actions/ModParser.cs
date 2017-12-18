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
            public string Author;
            public string Section;
            public bool CommandOnly;
            public List<string> Dependencies;

            public JSMod()
            {
                FileName = "";
                Name = "Unnamed";
                Section = "None";
                CommandOnly = false;
                Dependencies = new List<string>();
            }

            public JSMod(string fileName, string name,string section, bool commandOnly, List<string> dependencies)
            {
                FileName = fileName;
                Name = name;
                Section = section;
                CommandOnly = commandOnly;
                Dependencies = dependencies ?? new List<string>();
            }
        }

        public List<JSMod> JSMods = new List<JSMod>();

        public JSMod DefaultMod = new JSMod();

        private string ParseName(string input)
        {
            string pattern = @"^(?:.*name)(?:.*[""|'])(.*)(?:""|')(?:,|;)";

            Match match = Regex.Match(input, pattern);
            if (match.Success && match.Groups.Count > 1)
                return match.Groups[1]?.Value;

            return null;
        }

        private string ParseSection(string input)
        {
            string pattern = @"^(?:.*section)(?:.*[""|'])(.*)(?:""|')(?:,|;)";

            Match match = Regex.Match(input, pattern, RegexOptions.CultureInvariant);
            if (match.Success && match.Groups.Count > 1)
                return match.Groups[1]?.Value;

            return null;
        }

        private List<string> ParseDependencies(string input)
        {
            List<string> list = new List<string>();

            string pattern = @"^(?:.*dependencies)(?:.*)\[(.*)\](?:,|;)";

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
                                list.Add(dep.Trim('"', '\''));

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

                    string name = "", section = "";
                    List<string> dependencies = null;

                    while (r.Peek() >= 0)
                    {
                        var line = Regex.Unescape(r.ReadLine());

                        if(String.IsNullOrEmpty(name))
                            name = ParseName(line);

                        if (String.IsNullOrEmpty(section))
                            section = ParseSection(line);

                        if(dependencies == null)
                            dependencies = ParseDependencies(line);
                    }

                    if (!String.IsNullOrEmpty(name))
                    {
                        return new JSMod(Path.GetFileNameWithoutExtension(file), name, section, true, dependencies);
                    }
                                          
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