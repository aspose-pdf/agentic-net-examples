using System;
using System.IO;
using Aspose.Pdf;               // For Document to read page count
using Aspose.Pdf.Facades;      // For PdfFileEditor facade

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";   // PDF that will receive pages
        const string sourcePdfPath = "source.pdf";   // PDF containing pages to insert
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Verify input files exist
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Determine all page numbers from the source PDF
        int[] sourcePageNumbers;
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            sourcePageNumbers = new int[srcDoc.Pages.Count];
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
                sourcePageNumbers[i - 1] = i;   // 1‑based page numbers
        }

        // Create the facade that performs the insertion
        PdfFileEditor editor = new PdfFileEditor();

        // Insert the source pages after page 1 of the target PDF.
        // Parameters: (targetFile, insertAfterPage, sourceFile, sourcePages[], outputFile)
        editor.Insert(targetPdfPath, 1, sourcePdfPath, sourcePageNumbers, outputPdfPath);

        Console.WriteLine($"Inserted pages from '{sourcePdfPath}' into '{targetPdfPath}'. Saved as '{outputPdfPath}'.");
    }
}