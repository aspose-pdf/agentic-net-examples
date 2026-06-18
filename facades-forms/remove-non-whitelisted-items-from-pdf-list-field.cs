using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Define the whitelist of allowed items for the "State" list field.
        string[] whitelist = { "California", "New York", "Texas" };

        // Complete list of possible items (example list of US states).
        string[] allItems = {
            "Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Delaware",
            "Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky",
            "Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi",
            "Missouri","Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico",
            "New York","North Carolina","North Dakota","Ohio","Oklahoma","Oregon","Pennsylvania",
            "Rhode Island","South Carolina","South Dakota","Tennessee","Texas","Utah","Vermont",
            "Virginia","Washington","West Virginia","Wisconsin","Wyoming"
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create FormEditor with source and destination PDF files.
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Remove every list item that is not in the whitelist.
        foreach (string item in allItems)
        {
            if (!whitelist.Contains(item))
            {
                formEditor.DelListItem("State", item);
            }
        }

        // Dispose FormEditor if it implements IDisposable.
        if (formEditor is IDisposable disposable)
        {
            disposable.Dispose();
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}