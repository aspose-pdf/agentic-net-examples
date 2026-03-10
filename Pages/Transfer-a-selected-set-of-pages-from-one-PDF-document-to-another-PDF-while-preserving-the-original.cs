using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (pages to copy), the target PDF (receives pages), and the output PDF.
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged_selected.pdf";

        // Define the pages to transfer (1‑based indexing as required by Aspose.Pdf).
        int[] pagesToTransfer = { 2, 4, 5 };

        // Verify that both input files exist.
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks to ensure proper disposal.
            using (Document sourceDoc = new Document(sourcePath))
            using (Document targetDoc = new Document(targetPath))
            {
                // Ensure the target document has at least one page (required for some operations).
                if (targetDoc.Pages.Count == 0)
                {
                    targetDoc.Pages.Add();
                }

                // Iterate over the selected page numbers.
                foreach (int pageNum in pagesToTransfer)
                {
                    // Validate the page number against the source document's page count.
                    if (pageNum < 1 || pageNum > sourceDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageNum} is out of range in source document.");
                        continue;
                    }

                    // Retrieve the page from the source (preserves original layout).
                    Page pageToCopy = sourceDoc.Pages[pageNum];

                    // Add the page to the target document. The layout, annotations, and resources are retained.
                    targetDoc.Pages.Add(pageToCopy);
                }

                // Save the resulting PDF. No SaveOptions needed for PDF output.
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Selected pages transferred and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}