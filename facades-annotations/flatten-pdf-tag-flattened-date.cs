using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF, flatten it, add custom metadata, and save the result
            using (Document doc = new Document(inputPath))
            {
                // Flatten form fields and annotations
                doc.Flatten();

                // Add a custom metadata entry "FlattenDate" with the current UTC timestamp (ISO 8601)
                doc.Info["FlattenDate"] = DateTime.UtcNow.ToString("o");

                // Optionally also set the built‑in modification date for better PDF viewer support
                doc.Info.ModDate = DateTime.UtcNow;

                // Save the flattened and tagged PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF with metadata saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
