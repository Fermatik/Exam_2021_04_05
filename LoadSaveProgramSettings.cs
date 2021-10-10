using System.IO;
using System.Xml.Serialization;

namespace CSharp_Exam_2021_04_05
{
    public class LoadSaveProgramSettings
    {
        public static string DirProgramSettings = @"ProgramSettings";
        public static string NameFileProgramSettings = @"programSettings.xml";
        public static string DirDictionaries = @"Dictionaries";         
        public ProgramSettings CurrentProgramSettings;

        public LoadSaveProgramSettings()
        {
            if (!Directory.Exists(DirProgramSettings))
                Directory.CreateDirectory(DirProgramSettings);
            if (!Directory.Exists(DirDictionaries))
                Directory.CreateDirectory(DirDictionaries);
            CurrentProgramSettings = new ProgramSettings();
        }

        public string PathFileProgramSettings { get => Path.Combine(DirProgramSettings, NameFileProgramSettings); }

        public void LoadProgramSettings()
        {
            if (!File.Exists(PathFileProgramSettings)) return;
            XmlSerializer formatter = new XmlSerializer(typeof(ProgramSettings));
            using (FileStream fs = new FileStream(PathFileProgramSettings, FileMode.Open))
            {
                CurrentProgramSettings = (ProgramSettings)formatter.Deserialize(fs);
            }
        }

        public void SaveProgramSettings()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ProgramSettings));
            using (FileStream fs = new FileStream(PathFileProgramSettings, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, CurrentProgramSettings);
            }
        }

        public static void SaveDictionary(string nameFileDictionary, MyDictionary myDictionary)
        {
            string pathFileDictionary = Path.Combine(DirDictionaries, nameFileDictionary);
            XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
            using (FileStream fs = new FileStream(pathFileDictionary, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, myDictionary);
            }
        }

        public static MyDictionary LoadDictionary(string nameFileDictionary)
        {
            MyDictionary result = null;
            string pathFileDictionary = Path.Combine(DirDictionaries, nameFileDictionary);
            if (!File.Exists(pathFileDictionary)) return result;
            XmlSerializer formatter = new XmlSerializer(typeof(MyDictionary));
            using (FileStream fs = new FileStream(pathFileDictionary, FileMode.Open))
            {
               result = (MyDictionary)formatter.Deserialize(fs);
            }
            return result;
        }

        public static string GenerateNewNameFileDictionary()
        {
            string result = null;
            string startName = "dictionary";
            string finishName = ".xml";
            string nameFile;
            if (!Directory.Exists(DirDictionaries)) return result;
            int minI = 0;
            int maxI = 9999;
            for (int i = minI; i < maxI; i++)
            {
                nameFile = $"{startName}{i:d4}{finishName}";
                if (!File.Exists(Path.Combine(DirDictionaries, nameFile)))
                {
                    result = nameFile;
                    break;
                }
            }
            return result;
        }
    }
}
