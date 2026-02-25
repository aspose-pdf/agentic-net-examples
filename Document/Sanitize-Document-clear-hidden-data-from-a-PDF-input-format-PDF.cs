using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove standard document metadata (author, title, etc.)
            doc.RemoveMetadata();

            // Remove PDF/A and PDF/UA compliance flags if present
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Delete all embedded files (attachments, OLE objects, etc.)
            doc.EmbeddedFiles.Delete();

            // Flatten form fields (if any) so that only their appearance remains
            doc.Flatten();

            // Optimize resources: remove unused objects and merge duplicates
            doc.OptimizeResources();

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}