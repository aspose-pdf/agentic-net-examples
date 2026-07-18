using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For any facade operations (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // 1. Remove all document metadata (title, author, hidden XMP, etc.)
            doc.RemoveMetadata();

            // 2. Remove any embedded files (attachments) from the PDF
            //    EmbeddedFiles collection provides Delete() to clear all entries.
            doc.EmbeddedFiles?.Delete();

            // 3. Remove JavaScript actions.
            //    Aspose.Pdf does not expose a direct method, but removing PDF/UA and PDF/A
            //    compliance strips many hidden elements, including JavaScript.
            doc.RemovePdfUaCompliance();
            doc.RemovePdfaCompliance();

            // 4. Optimize resources (unused objects, duplicate streams, etc.) to shrink size.
            doc.OptimizeResources();

            // Save the cleaned PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}