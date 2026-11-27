using System;
using System.IO;
using System.Linq;
using System.Drawing.Text;

class Program
{
    static void Main()
    {
        // Retrieve all installed system fonts
        var fontCollection = new InstalledFontCollection();
        string[] fontNames = fontCollection.Families
            .Select(f => f.Name)
            .ToArray();

        // Build a single CSV line; each name is quoted and escaped
        string csvLine = string.Join(
            ",",
            fontNames.Select(name => $"\"{name.Replace("\"", "\"\"")}\"")
        );

        // Write the CSV line to Fonts.csv
        File.WriteAllText("Fonts.csv", csvLine + Environment.NewLine);

        Console.WriteLine("Fonts.csv has been created.");
    }
}
