using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string modifiedPath = "modified.pdf";
        const string diffOutputPath = "diff.pdf";

        if (!File.Exists(originalPath) || !File.Exists(modifiedPath))
        {
            Console.Error.WriteLine("Both input PDF files must exist.");
            return;
        }

        // Load the two documents to be compared
        using (Document original = new Document(originalPath))
        using (Document modified = new Document(modifiedPath))
        {
            // Comparison options – using defaults (highlight style, case‑sensitivity, etc.)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison; result is a list of differences per page
            List<List<DiffOperation>> pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(original, modified, options);

            // Generate a PDF that visualises the differences. The generator uses the default
            // highlight colours (red for deletions, green for insertions) as documented.
            PdfOutputGenerator generator = new PdfOutputGenerator();
            generator.GenerateOutput(pageDiffs, diffOutputPath);
        }

        // Simple verification – load the generated diff PDF and confirm it was created.
        using (Document diffDoc = new Document(diffOutputPath))
        {
            Console.WriteLine($"Diff PDF created successfully: {diffOutputPath} (Pages: {diffDoc.Pages.Count})");
            // The default highlight colours are applied automatically; you can inspect annotations
            // or appearance streams if deeper verification is required.
        }
    }
}