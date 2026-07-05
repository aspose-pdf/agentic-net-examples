using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, remove existing Bates numbering, then add a new sequence
        using (Document doc = new Document(inputPath))
        {
            // Delete all Bates numbering artifacts from every page
            doc.Pages.DeleteBatesNumbering();

            // Add new Bates numbering with desired configuration
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber      = 1;          // starting number
                artifact.Prefix           = "BN-";       // optional prefix
                artifact.NumberOfDigits  = 6;          // zero‑padded to 6 digits
                artifact.IsBackground    = false;      // place on top of page content
                // Additional properties (e.g., Position, Alignment) can be set here if needed
            });

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering updated and saved to '{outputPath}'.");
    }
}