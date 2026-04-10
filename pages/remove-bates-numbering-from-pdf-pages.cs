using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Delete all Bates numbering artifacts from the pages collection.
            // This removes any Bates stamp that was added to the document.
            doc.Pages.DeleteBatesNumbering();

            // Save the modified PDF. No SaveOptions are needed because the output
            // format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering removed. Saved to '{outputPath}'.");
    }
}