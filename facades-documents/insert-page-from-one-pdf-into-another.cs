using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "source.pdf";   // PDF that contains the page to copy (page 2)
        const string targetPdfPath = "target.pdf";   // PDF into which the page will be inserted
        const string outputPdfPath = "output.pdf";   // Resulting PDF

        // Verify that source and target files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        // Copy the original target PDF to the output location.
        // We will modify the copy so the original stays untouched.
        File.Copy(targetPdfPath, outputPdfPath, true);

        // Load the documents
        Document targetDoc = new Document(outputPdfPath);
        Document sourceDoc = new Document(sourcePdfPath);

        // Determine the position where the new page will be inserted.
        // Here we insert after the last existing page (i.e., at the end of the document).
        int insertPosition = targetDoc.Pages.Count + 1; // 1‑based indexing

        // Insert page 2 from the source PDF into the target PDF.
        // Pages.Insert(pageNumber, sourcePage) inserts a single page.
        // sourceDoc.Pages is 1‑based, so page 2 is sourceDoc.Pages[2].
        targetDoc.Pages.Insert(insertPosition, sourceDoc.Pages[2]);

        // Save the modified document back to the output path.
        targetDoc.Save(outputPdfPath);

        Console.WriteLine($"Page 2 from '{sourcePdfPath}' inserted into '{outputPdfPath}' at position {insertPosition}.");
    }
}
