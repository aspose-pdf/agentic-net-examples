using System;
using System.IO;
using Aspose.Pdf; // Document, PageCollectionExtensions, BatesNArtifact

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page with an alphanumeric prefix
            doc.Pages.AddBatesNumbering(bates =>
            {
                bates.Prefix = "PRJ-";      // alphanumeric prefix for project tracking
                bates.StartNumber = 1;      // start numbering at 1
                bates.NumberOfDigits = 5;   // zero‑padded to 5 digits (e.g., 00001)
            });

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}