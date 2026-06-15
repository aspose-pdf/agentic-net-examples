using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing existing Bates numbering artifacts
        const string inputPath  = "input.pdf";
        // Output PDF after removing old Bates numbers and adding new ones
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Remove all existing Bates numbering artifacts from every page
            // -----------------------------------------------------------------
            // PageCollectionExtensions provides the DeleteBatesNumbering extension method.
            doc.Pages.DeleteBatesNumbering();

            // -----------------------------------------------------------------
            // 2. Add new Bates numbering to each page
            // -----------------------------------------------------------------
            // Configure the BatesNArtifact via an Action delegate.
            doc.Pages.AddBatesNumbering(artifact =>
            {
                // Starting number for the sequence
                artifact.StartNumber = 1;
                // Optional prefix to appear before the number
                artifact.Prefix = "DOC-";
                // Number of digits (padded with leading zeros if needed)
                artifact.NumberOfDigits = 5;
                // Position can be set via margins or explicit rectangle.
                // Here we use default margins; you can also set Position directly.
                artifact.StartPage = 1; // first page to apply numbering
                artifact.EndPage = 0;   // 0 means no upper bound (apply to all pages)
                // Make the numbering appear in the foreground (over page content)
                artifact.IsBackground = false;
                // Optional opacity (0.0 to 1.0)
                artifact.Opacity = 1.0;
                // You can also set alignment, margins, etc., if needed:
                // artifact.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                // artifact.BottomMargin = 20;
            });

            // -----------------------------------------------------------------
            // 3. Save the modified document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering updated and saved to '{outputPath}'.");
    }
}