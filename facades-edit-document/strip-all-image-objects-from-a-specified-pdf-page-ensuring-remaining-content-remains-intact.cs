using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF
        const int  pageNumber  = 2;              // page to clean (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to discover how many images are on the target page
        using (Document doc = new Document(inputPath))
        {
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Count images on the specified page
            int imageCount = doc.Pages[pageNumber].Resources.Images.Count;

            // Build an array of image indexes (1‑based as required by DeleteImage)
            int[] indexes = Enumerable.Range(1, imageCount).ToArray();

            // Use PdfContentEditor (facade) to delete the images
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPath);                     // load PDF into the facade
                if (indexes.Length > 0)
                {
                    editor.DeleteImage(pageNumber, indexes);   // delete all images on the page
                }
                editor.Save(outputPath);                       // persist changes
            }
        }

        Console.WriteLine($"All images removed from page {pageNumber}. Output saved to '{outputPath}'.");
    }
}