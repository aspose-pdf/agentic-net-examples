using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove standard metadata (author, title, etc.)
            doc.RemoveMetadata();

            // Remove PDF/A and PDF/UA compliance flags if present
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Delete all embedded files (if any)
            doc.EmbeddedFiles.Delete();

            // Delete all outline/bookmark entries
            doc.Outlines.Delete();

            // Optimize resources: remove unused objects and merge duplicates
            doc.OptimizeResources();

            // Flatten transparency to eliminate hidden transparent content
            doc.FlattenTransparency();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden data cleared. Saved to '{outputPath}'.");
    }
}