using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to insert
        const string targetPdfPath = "target.pdf";   // PDF into which the page will be inserted
        const string outputPdfPath = "merged.pdf";   // Resulting PDF
        const int insertPosition = 2;                // Position (1‑based) where the page will be inserted

        // Verify files exist
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

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Choose the page to insert from the source document (first page in this example)
            Page pageToInsert = sourceDoc.Pages[1];

            // Insert the page into the target document at the specified position
            // PageCollection.Insert expects a 1‑based index
            targetDoc.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified target document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdfPath}'.");
    }
}