using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dictionary
{
    internal class Words
    {
        public string Word { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }

        public string ImagePath { get; set; }

        // Constructor
        public Words(string name, string description, string category, string imagePath)
        {
            Word = name;
            Description = description;
            Category = category;
            ImagePath = imagePath;
        }

        // Method to read words from JSON file
        public static List<Words> ReadWordsFromJson()
        {
            List<Words> wordsList;
            if (File.Exists("words.json"))
            {
                string json = File.ReadAllText("words.json");
                wordsList = JsonConvert.DeserializeObject<List<Words>>(json);
            }
            else
            {
                wordsList = new List<Words>();
            }
            return wordsList;
        }

        // Method to write words to JSON file
        public static void WriteWordsToJson(List<Words> wordsList)
        {
            string updatedJson = JsonConvert.SerializeObject(wordsList, Formatting.Indented);
            File.WriteAllText("words.json", updatedJson);
        }

        public void AddWord(string name, string description, string category, string imagePath)
        {
            List<Words> wordsList = ReadWordsFromJson();
            wordsList.Add(new Words(name, description, category, imagePath));
            WriteWordsToJson(wordsList);
        }
    }
    
}
