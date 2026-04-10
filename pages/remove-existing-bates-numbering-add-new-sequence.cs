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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove any existing Bates numbering artifacts from all pages
            doc.Pages.DeleteBatesNumbering();

            // Add new Bates numbering with desired settings
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber      = 1;          // start numbering at 1
                artifact.Prefix           = "BN-";      // optional prefix
                artifact.NumberOfDigits   = 6;          // e.g., 000001, 000002, ...
                artifact.IsBackground     = false;      // stamp appears in front of content
                // Additional properties (e.g., Position, Alignment) can be set here if needed
            });

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}