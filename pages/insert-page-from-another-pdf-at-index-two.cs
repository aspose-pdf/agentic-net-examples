using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the existing PDFs
        const string targetPdfPath = "target.pdf";   // PDF into which the page will be inserted
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to insert
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(targetPdfPath) || !File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Open both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Retrieve the page to be inserted (e.g., the first page of the source PDF)
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page at index 2 (Aspose.Pdf uses 1‑based indexing)
            // The Insert method copies the page together with its size and rotation.
            targetDoc.Pages.Insert(2, pageToInsert);

            // Save the modified document as a PDF
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdfPath}'.");
    }
}