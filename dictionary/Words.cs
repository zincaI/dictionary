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
        public Words()
        {

        }
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "words.json");
            List<Words> wordsList;
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    wordsList = JsonConvert.DeserializeObject<List<Words>>(json);

                }
                else
                {
                    wordsList = new List<Words>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
                wordsList = new List<Words>();
            }
            return wordsList;
        }

        public static void WriteWordsToJson(List<Words> wordsList)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "words.json");
            try
            {
                string updatedJson = JsonConvert.SerializeObject(wordsList, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to JSON file: " + ex.Message);
            }
        }



        public void AddWord(string word, string description, string category, string imagePath)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "words.json");
            List<Words> wordsList = ReadWordsFromJson();
            wordsList.Add(new Words(word, description, category, imagePath));
            WriteWordsToJson(wordsList);
        }
    }
    
}
