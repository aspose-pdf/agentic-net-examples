using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to each page with custom prefix and suffix
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix = "DOC";      // Custom prefix
                artifact.Suffix = "-2026";    // Custom suffix
                artifact.StartNumber = 1;     // Optional: start numbering at 1
            });

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates-numbered PDF saved to '{outputPath}'.");
    }
}