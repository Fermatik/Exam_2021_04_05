using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CSharp_Exam_2021_04_05
{



    class ProgramFile
    {
        private string pathDir;        
        public List<Dictionary> dictionaries;

        public string PathDirProgramData
        {
            set
            {
                if (!Directory.Exists(value))
                    Directory.CreateDirectory(value);
                this.pathDir = value;
            }
            get => this.pathDir;
        }

        public string NameFileProgramData { set; get; }
                public string PathFileProgramData
        {
            get => Path.Combine(PathDirProgramData, NameFileProgramData);
        }

        public ProgramFile(string pathDir, string nameFile)
        {
            this.PathDirProgramData = pathDir;
            this.NameFileProgramData = nameFile;
            this.dictionaries = new List<Dictionary>();
        }

        public void LoadProgramDataFromFile()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Dictionary>));
            using (FileStream fs = new FileStream(PathFileProgramData, FileMode.Open, FileAccess.Read))
            {
                dictionaries = (List<Dictionary>)formatter.Deserialize(fs);
            }
        }

        public void SaveProgramDataToFile()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Dictionary>));
            using (FileStream fs = new FileStream(PathFileProgramData, FileMode.OpenOrCreate))
            {
               formatter.Serialize(fs, this.dictionaries);                
            }
        }

    }
}

