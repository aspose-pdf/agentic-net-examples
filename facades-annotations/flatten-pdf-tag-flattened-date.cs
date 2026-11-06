using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, flatten it, add metadata, and save.
        using (Document doc = new Document(inputPath))
        {
            // Flatten annotations and form fields.
            doc.Flatten();

            // ISO 8601 formatted UTC date/time for the flattening operation.
            string flattenDate = DateTime.UtcNow.ToString("o");

            // Store a custom metadata entry named "FlattenDate".
            doc.Info["FlattenDate"] = flattenDate;

            // Optionally set the standard modification date as well.
            doc.Info.ModDate = DateTime.UtcNow;

            // Save the flattened PDF with the new metadata.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved with metadata at '{outputPath}'.");
    }
}
