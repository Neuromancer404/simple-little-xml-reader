using FRLLO_generic_parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRLLO_parser
{
    internal class XmlReader
    {
        public int RootTagCount;
        private string _Path;
        private string _rootTag;
        private string _stratcherInfo;
        private string[] nodeNline = new string[2];
        public int Create(string Path, string rootTag)
        {
            _Path = Path;
            _rootTag = rootTag;
            int i = 0, j = 0;
            foreach (string line in System.IO.File.ReadLines(Path))
            {
                if (line.Contains("<" + _rootTag + ">"))
                {
                    i++;
                }
                if (line.Contains("</" + _rootTag + ">"))
                {
                    j++;
                }
            }
            if (i == j)
            {
                RootTagCount =j;
            }

            return RootTagCount;
        }

        public List<Dictionary<string, string>> Read(string tag = null)
        {
            List<Dictionary<string, string>> lines = new List<Dictionary<string, string>>();

            using (StreamReader reader = new StreamReader(_Path))
            {
                string line; bool check = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (tag != null && line.Contains("<" + tag + ">") && line.Contains("</" + tag + ">"))
                    {
                        string[] answr = lineReader(line, 0);
                        _stratcherInfo = answr[1];
                    }
                    else
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        if (line.Contains("<" + _rootTag + ">"))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains("</" + _rootTag + ">"))
                                {
                                    break;
                                }
                                string[] a = lineReader(line, 0);
                                if (a[1] != "" && a[1] != null)
                                {
                                    dict.Add(a[0], a[1]);
                                }
                                nodeNline[0] = ""; nodeNline[1] = "";
                            }
                            lines.Add(dict);
                        }
                    }
                }
            }
            return lines;
        }

        private string[] lineReader(string line, int index, string nodeBegin = "<", string nodeEnd = ">")
        {
            int fu = line.IndexOf(nodeBegin);
            int su = line.IndexOf(nodeEnd);
            string data = "";

            for (int i = fu + 1; i < su; i++)
            {
                data += line[i];
            }
            nodeNline[index] = data;
            if (line.Contains($"</{data}>"))
            {
                lineReader(line, 1, ">", "</" + data + ">");
            }
            return nodeNline;
        }

        public string Stracher(string tag)
        {
            Read(tag);
            return _stratcherInfo;
        }
    }
}
