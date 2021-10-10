using System;

namespace CSharp_Exam_2021_04_05
{
    [Serializable]
    public class Dictionary
    {
        public string NameDictionary { set; get; }
        public string TypeDictionary { set; get; }
        public string NameFileDictionary { set; get; }

        public Dictionary()
        {

        }

        public Dictionary(string nameDictionary, string typeDictionary, string nameFileDictionary)
        {
            this.NameDictionary = nameDictionary;
            this.TypeDictionary = typeDictionary;
            this.NameFileDictionary = nameFileDictionary;
        }

        public override string ToString()
        {
            return $"{NameDictionary}  {TypeDictionary}  {NameFileDictionary}"; 
        }
    }
}

