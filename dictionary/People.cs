using System.IO;

internal class People
{
    public string Name { get; set; }
    public string Password { get; set; }

    public People(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public static bool FindAdmin(string name, string password)
    {
        string filePath = "People.json";

        
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("People.json file not found.");
        }

        
        string json = File.ReadAllText(filePath);

       
        People[] peopleArray = Newtonsoft.Json.JsonConvert.DeserializeObject<People[]>(json);

   
        foreach (var person in peopleArray)
        {
            if (person.Name == name && person.Password == password)
            {
                return true; 
            }
        }

        return false; 
    }
}
