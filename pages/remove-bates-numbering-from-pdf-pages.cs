using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Path to the source PDF
        const string outputPath = "output.pdf";  // Path for the resulting PDF

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete all Bates numbering artifacts from the pages.
            // The DeleteBatesNumbering extension method operates on the PageCollection.
            // If only specific pages contain the Bates stamp, this call will effectively
            // remove the stamp from those pages while leaving other pages unchanged.
            doc.Pages.DeleteBatesNumbering();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering removed. Output saved to '{outputPath}'.");
    }
}