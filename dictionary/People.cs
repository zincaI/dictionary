using System.IO;

internal class People
{
    public string Name { get; set; }
    public string Password { get; set; }

    // Constructor
    public People(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public static bool FindAdmin(string name, string password)
    {
        string filePath = "People.json";

        // Check if the file exists at the specified path
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("People.json file not found.");
        }

        // Read JSON data from file
        string json = File.ReadAllText(filePath);

        // Deserialize JSON data into an array of People objects
        People[] peopleArray = Newtonsoft.Json.JsonConvert.DeserializeObject<People[]>(json);

        // Search for the name-password pair in the array
        foreach (var person in peopleArray)
        {
            if (person.Name == name && person.Password == password)
            {
                return true; // Found the matching name-password pair
            }
        }

        return false; // No matching name-password pair found
    }
}
