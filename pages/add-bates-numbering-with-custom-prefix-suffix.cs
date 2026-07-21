using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page with custom prefix and suffix
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "DOC";      // custom prefix
                artifact.Suffix = "-2026";    // custom suffix
                artifact.StartNumber = 1;     // start from 1 (default)
                // artifact.NumberOfDigits = 6; // default number of digits
            });

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}