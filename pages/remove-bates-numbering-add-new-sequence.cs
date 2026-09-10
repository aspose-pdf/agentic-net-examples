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

        // Load the PDF, remove existing Bates numbering, add new numbering, and save.
        using (Document doc = new Document(inputPath))
        {
            // Delete all existing Bates numbering artifacts from every page.
            doc.Pages.DeleteBatesNumbering();

            // Add new Bates numbering to each page.
            // Configure the artifact via the provided Action<BatesNArtifact>.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.StartNumber   = 1;          // Starting number.
                artifact.Prefix        = "DOC-";     // Optional prefix.
                artifact.NumberOfDigits = 6;         // Number of digits (e.g., 000001).
                // Additional properties can be set as needed, e.g.:
                // artifact.Position = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                // artifact.IsBackground = false;
            });

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering updated and saved to '{outputPath}'.");
    }
}