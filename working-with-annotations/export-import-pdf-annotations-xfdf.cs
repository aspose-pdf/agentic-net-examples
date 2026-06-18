using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfFile = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the original PDF and count its annotations
        int originalCount;
        using (Document originalDoc = new Document(inputPdf))
        {
            originalCount = CountAnnotations(originalDoc);
            // Export all annotations to an XFDF file
            originalDoc.ExportAnnotationsToXfdf(xfdfFile);
        }

        // Load a fresh copy of the same PDF (without the exported annotations)
        int importedCount;
        using (Document importedDoc = new Document(inputPdf))
        {
            // Import annotations from the XFDF file
            importedDoc.ImportAnnotationsFromXfdf(xfdfFile);
            importedCount = CountAnnotations(importedDoc);
        }

        // Verify round‑trip integrity by comparing annotation counts
        if (originalCount == importedCount)
        {
            Console.WriteLine($"Success: annotation count matches ({originalCount}).");
        }
        else
        {
            Console.WriteLine($"Mismatch: original={originalCount}, after import={importedCount}.");
        }
    }

    // Helper method to count all annotations in a document
    static int CountAnnotations(Document doc)
    {
        int count = 0;
        foreach (Page page in doc.Pages)
        {
            count += page.Annotations.Count;
        }
        return count;
    }
}