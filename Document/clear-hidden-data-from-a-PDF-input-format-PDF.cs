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
            // Remove document metadata (title, author, custom data, etc.)
            doc.RemoveMetadata();

            // Delete all embedded files (if any)
            doc.EmbeddedFiles.Delete();

            // Delete all outline (bookmark) entries
            doc.Outlines.Delete();

            // Delete any Bates numbering artifacts from all pages
            doc.Pages.DeleteBatesNumbering();

            // Remove PDF/A and PDF/UA compliance flags, if present
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Optimize resources to discard unused objects and compress the file
            doc.OptimizeResources();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden data cleared. Saved to '{outputPath}'.");
    }
}