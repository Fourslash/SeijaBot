using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SkypeBot2
{
    class XmlStringsArray
    {
        public XmlStringsArray(string Path)
        {
            path = Path;
            Read();
        }
        List<string> strings;
        public List<string> Strings
        {
            get { return strings; }
            set
            {
                strings = value;
                Write();
            }
        }
        public void AddString(string str)
        {
            strings.Add(str);
            Write();
        }
        string path;
        public void Write()
        {
            if (!File.Exists(path))
                using (File.Create(path)) ;
            var serializer = new XmlSerializer(typeof(List<string>));
            var sw = new StreamWriter(path);
            serializer.Serialize(sw, strings);
            sw.Close();
        }
        public void Read()
        {
            if (!File.Exists(path))
                using (File.Create(path)) ;
            var stream = new StreamReader(path);
            if (stream.BaseStream.Length != 0)
            {
                var ser = new XmlSerializer(typeof(List<string>));
                strings = (List<string>)ser.Deserialize(stream);
            }
            else
            {
                strings = new List<string>();
            }
            stream.Close();
        }
        public string GetRandomString()
        {
            if (strings.Count < 1)
                return "Массив строк пуст (✿◕⁀◕)";
            return strings[SeijaHelper.FixedRandom(0, strings.Count - 1)];
        }
    }
}
