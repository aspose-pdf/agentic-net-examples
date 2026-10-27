using System;
using System.IO;
using System.Xml.Linq;

class Program
{
    // Entry point of the application
    static void Main(string[] args)
    {
        // Example: obtain progress value (could be from a UI control, argument, etc.)
        // Here we simply parse the first command‑line argument or default to 0.
        int progressValue = 0;
        if (args.Length > 0 && int.TryParse(args[0], out int parsed))
        {
            progressValue = Math.Clamp(parsed, 0, 100); // ensure 0‑100 range
        }

        // Build the XML structure
        XDocument xmlDoc = new XDocument(
            new XElement("Progress",
                new XElement("Value", progressValue),
                new XElement("Timestamp", DateTime.UtcNow.ToString("o")) // ISO 8601 format
            )
        );

        // Define the output path (can be customized)
        string outputPath = "progress_status.xml";

        // Write the XML to file using a FileStream inside a using block for deterministic disposal
        using (FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            xmlDoc.Save(stream);
        }

        Console.WriteLine($"Progress value {progressValue} saved to '{outputPath}'.");
    }
}