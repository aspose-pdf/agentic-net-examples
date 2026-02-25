using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPath    = "first_pages_comparison.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load both source documents and create a result document.
        // All Document objects are wrapped in using blocks for deterministic disposal.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        using (Document result = new Document())
        {
            // Aspose.Pdf uses 1‑based page indexing (see page-indexing-one-based rule).
            // Add the first page of each source document to the result document.
            result.Pages.Add(doc1.Pages[1]);   // First page of the first PDF
            result.Pages.Add(doc2.Pages[1]);   // First page of the second PDF

            // Save the combined document as PDF.
            result.Save(outputPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
    }
}