using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to append
        const string targetPdfPath = "target.pdf";   // PDF to which the page will be appended
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Verify that both input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            // Retrieve the page to be appended from the source document.
            Page pageToAppend = sourceDoc.Pages[1];

            // Append the page to the end of the target document.
            // This adds the page without altering its original content.
            targetDoc.Pages.Add(pageToAppend);

            // Save the combined document as PDF.
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page appended successfully. Output saved to '{outputPdfPath}'.");
    }
}