using System;
using System.Collections.Generic;

namespace CSharp_Exam_2021_04_05
{
    [Serializable]
    public class ProgramSettings
    {            
        public List<Dictionary> Dictionaries;

        public int CountDictionaries { get => Dictionaries.Count; }
        public ProgramSettings()
        {
            this.Dictionaries = new List<Dictionary>();
        }        
    }
}
