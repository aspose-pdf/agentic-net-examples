using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string targetPdfPath = "target.pdf";   // PDF that will receive the new page
        const string sourcePdfPath = "source.pdf";   // PDF containing the page to insert
        const string outputPdfPath = "merged.pdf";   // Resulting PDF

        // Position (1‑based) where the page will be inserted in the target PDF.
        // For example, 3 means the new page becomes the third page.
        const int insertPosition = 3;

        // Verify files exist
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

        // Load both documents inside using blocks for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Ensure the source document has at least one page
            if (sourceDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Source PDF does not contain any pages.");
                return;
            }

            // Get the page to insert (first page of source PDF in this example)
            Page pageToInsert = sourceDoc.Pages[1]; // 1‑based indexing

            // Insert the page into the target document at the desired position
            // Pages.Insert expects a 1‑based index where the new page will appear.
            targetDoc.Pages.Insert(insertPosition, pageToInsert);

            // Save the modified document
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPdfPath}'.");
    }
}