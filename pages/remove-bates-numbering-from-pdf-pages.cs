using System;
using System.IO;
using Aspose.Pdf; // PageCollectionExtensions.DeleteBatesNumbering is defined here

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete all Bates numbering artifacts from the pages collection.
            // This removes any Bates numbering stamps that were added.
            doc.Pages.DeleteBatesNumbering();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering removed. Saved to '{outputPath}'.");
    }
}