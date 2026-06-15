using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, PageCollection, BatesNArtifact)
 
class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Source PDF
        const string outputPath = "bates_numbered.pdf"; // Destination PDF
        const string prefix     = "PRJ-";               // Alphanumeric prefix for tracking
 
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
 
        // Load the PDF inside a using block for deterministic disposal (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Add Bates numbering to every page.
            // The AddBatesNumbering extension accepts an Action<BatesNArtifact>
            // where we configure the artifact (prefix, start number, digit count, etc.).
            doc.Pages.AddBatesNumbering(artifact =>
            {
                artifact.Prefix          = prefix;   // e.g., "PRJ-"
                artifact.StartNumber     = 1;        // Starting number
                artifact.NumberOfDigits = 5;        // Total digits (including leading zeros)
                // Optional: customize placement, margins, alignment, etc.
                // artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Right;
                // artifact.BottomMargin = 20;
            });
 
            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }
 
        Console.WriteLine($"Bates numbering applied and saved to '{outputPath}'.");
    }
}