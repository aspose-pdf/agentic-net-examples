using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with annotations
        const string xfdfPath = "annotations.xfdf";       // temporary XFDF file
        const string outputPdfPath = "output.pdf";       // PDF after re‑import

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1 – Load the source document and export its annotations
        // ------------------------------------------------------------
        int originalAnnotationCount = 0;
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Count all annotations in the source document
            foreach (Page page in srcDoc.Pages)
                originalAnnotationCount += page.Annotations.Count;

            // Export annotations to XFDF file
            srcDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // ------------------------------------------------------------
        // Step 2 – Load a fresh copy of the PDF, clear existing annotations,
        //          then import the previously exported XFDF data
        // ------------------------------------------------------------
        int importedAnnotationCount = 0;
        using (Document targetDoc = new Document(inputPdfPath))
        {
            // Optional: remove existing annotations to ensure a clean round‑trip
            foreach (Page page in targetDoc.Pages)
                page.Annotations.Clear(); // <-- Fixed: use Clear() instead of DeleteAll()

            // Import annotations from the XFDF file
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Count annotations after import
            foreach (Page page in targetDoc.Pages)
                importedAnnotationCount += page.Annotations.Count;

            // Save the document that now contains the re‑imported annotations
            targetDoc.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // Step 3 – Verify round‑trip integrity
        // ------------------------------------------------------------
        Console.WriteLine($"Original annotation count : {originalAnnotationCount}");
        Console.WriteLine($"Imported annotation count : {importedAnnotationCount}");
        Console.WriteLine(importedAnnotationCount == originalAnnotationCount
            ? "Round‑trip successful: annotation counts match."
            : "Round‑trip failed: annotation counts differ.");
    }
}
