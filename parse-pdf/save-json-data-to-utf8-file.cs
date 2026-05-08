using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // Example JSON string containing form data
        string jsonData = @"{
    ""FirstName"": ""John"",
    ""LastName"": ""Doe"",
    ""Email"": ""john.doe@example.com""
}";

        // Path to the output file (UTF‑8 encoded)
        const string outputPath = "formData.json";

        // Write the JSON string to the file using UTF‑8 encoding
        File.WriteAllText(outputPath, jsonData, Encoding.UTF8);

        Console.WriteLine($"JSON data saved to '{outputPath}' with UTF‑8 encoding.");
    }
}