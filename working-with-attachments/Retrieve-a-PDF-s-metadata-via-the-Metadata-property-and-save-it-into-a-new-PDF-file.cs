using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document src = new Document(inputPath))
        {
            // Create a new empty PDF document
            using (Document dest = new Document())
            {
                // Copy all metadata entries from source to destination
                foreach (var entry in src.Metadata)
                {
                    // Metadata implements IDictionary<string, object>
                    dest.Metadata.Add(entry.Key, entry.Value);
                }

                // Save the new PDF with the copied metadata
                dest.Save(outputPath);
            }
        }

        Console.WriteLine($"Metadata copied to '{outputPath}'.");
    }
}