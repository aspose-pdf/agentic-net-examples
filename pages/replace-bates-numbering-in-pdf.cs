using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove any existing Bates numbering artifacts from all pages
            doc.Pages.DeleteBatesNumbering();

            // Add new Bates numbering with the desired configuration
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber = 1;                 // start numbering at 1
                artifact.Prefix = "DOC-";                 // optional prefix
                artifact.NumberOfDigits = 5;              // e.g., 00001, 00002, ...
                // Position expects a Point (lower‑left corner of the stamp)
                artifact.Position = new Aspose.Pdf.Point(0, 0);
                artifact.IsBackground = false;            // stamp appears on top of content
            });

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
