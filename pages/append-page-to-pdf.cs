using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For PdfPageStamp if needed (not used here)

class AppendPageExample
{
    static void Main()
    {
        // Paths – adjust as needed
        const string targetPdfPath   = "target.pdf";   // PDF to which the page will be appended
        const string sourcePdfPath   = "source.pdf";   // PDF containing the page to copy
        const string outputPdfPath   = "merged.pdf";   // Resulting PDF

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

        try
        {
            // Load the target document (the one we will modify)
            using (Document targetDoc = new Document(targetPdfPath))
            // Load the source document (the one providing the page)
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Choose the page to copy from the source document.
                // Aspose.Pdf uses 1‑based indexing, so page 1 is the first page.
                // Adjust the index if a different page is required.
                const int sourcePageIndex = 1;
                if (sourcePageIndex < 1 || sourcePageIndex > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid source page index.");
                    return;
                }

                // Retrieve the page from the source document.
                Page pageToAppend = sourceDoc.Pages[sourcePageIndex];

                // Append the page to the end of the target document.
                // The Add method copies the page content; the original source page remains unchanged.
                targetDoc.Pages.Add(pageToAppend);

                // Save the modified target document.
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Page {1} from '{sourcePdfPath}' appended to '{targetPdfPath}'.");
            Console.WriteLine($"Result saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}