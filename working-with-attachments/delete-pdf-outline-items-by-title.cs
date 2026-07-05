using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API

class Program
{
    static void Main()
    {
        const string inputPath  = "portfolio.pdf";
        const string outputPath = "portfolio_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The document outline (bookmarks) is accessed via the Outlines collection
            OutlineCollection outlines = doc.Outlines;

            // Example description text to match – delete any outline item whose title equals this text
            string descriptionToDelete = "Obsolete Portfolio Item";

            // Delete by title using the overload that accepts a string.
            // This removes the matching outline entry regardless of its index.
            outlines.Delete(descriptionToDelete);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}