using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // JSON string containing form data
        string json = "{\"name\":\"John Doe\",\"email\":\"john.doe@example.com\",\"age\":30}";

        // Path of the output file
        string outputPath = "formData.json";

        // Save the JSON string to a UTF‑8 encoded file
        File.WriteAllText(outputPath, json, Encoding.UTF8);

        Console.WriteLine($"JSON data saved to '{outputPath}'.");
    }
}