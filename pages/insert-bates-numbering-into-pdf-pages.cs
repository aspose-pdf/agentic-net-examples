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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page
            // StartNumber = 1000, Suffix = "-" (e.g., 1000-, 1001-, ...)
            // NumberOfDigits set to 4 to keep leading zeros if needed
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber = 1000;
                artifact.Suffix = "-";
                artifact.NumberOfDigits = 4;
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering applied. Saved to '{outputPath}'.");
    }
}