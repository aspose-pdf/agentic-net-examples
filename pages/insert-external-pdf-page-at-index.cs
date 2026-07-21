using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the target PDF (where the page will be inserted),
        // the source PDF (page to insert), and the resulting PDF.
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist.
        if (!File.Exists(targetPath) || !File.Exists(sourcePath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal.
        using (Document targetDoc = new Document(targetPath))
        using (Document sourceDoc = new Document(sourcePath))
        {
            // Retrieve the page to be inserted from the source document.
            // Aspose.Pdf uses 1‑based indexing for pages.
            Page sourcePage = sourceDoc.Pages[1];

            // Insert the source page into the target document at index 2.
            // The Insert method returns the reference to the newly inserted page.
            Page insertedPage = targetDoc.Pages.Insert(2, sourcePage);

            // Preserve the original size (MediaBox/CropBox) and rotation of the source page.
            insertedPage.MediaBox = sourcePage.MediaBox;
            insertedPage.CropBox  = sourcePage.CropBox;
            insertedPage.Rotate   = sourcePage.Rotate;

            // Save the modified document.
            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Page inserted successfully. Output saved to '{outputPath}'.");
    }
}