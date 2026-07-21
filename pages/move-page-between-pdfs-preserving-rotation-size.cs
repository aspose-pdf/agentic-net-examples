using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (where the page comes from),
        // the target PDF (where the page will be placed),
        // and the resulting PDF file.
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        // The 1‑based index of the page to move from the source document.
        const int pageNumberToMove = 2;

        // Verify that both input files exist.
        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF file not found.");
            return;
        }

        // Load both documents inside using blocks to ensure proper disposal.
        using (Document sourceDoc = new Document(sourcePath))
        using (Document targetDoc = new Document(targetPath))
        {
            // Validate the requested page number.
            if (pageNumberToMove < 1 || pageNumberToMove > sourceDoc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number specified.");
                return;
            }

            // Retrieve the page from the source document.
            Page pageToTransfer = sourceDoc.Pages[pageNumberToMove];

            // Add the page to the target document.
            // Add(Page) copies the page, preserving its size and rotation.
            targetDoc.Pages.Add(pageToTransfer);

            // If you truly want to *move* (i.e., remove from source), uncomment:
            // sourceDoc.Pages.Delete(pageNumberToMove);

            // Save the modified target document.
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page {pageNumberToMove} has been transferred to '{outputPath}'.");
    }
}