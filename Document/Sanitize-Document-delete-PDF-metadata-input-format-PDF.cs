using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, remove its metadata, and save the sanitized version.
        using (Document doc = new Document(inputPath))
        {
            // Removes all metadata (title, author, etc.) from the document.
            doc.RemoveMetadata();

            // Save the cleaned PDF. No SaveOptions needed because the format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}