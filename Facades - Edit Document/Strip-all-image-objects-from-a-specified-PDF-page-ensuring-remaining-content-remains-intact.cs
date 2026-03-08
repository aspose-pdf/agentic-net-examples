using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the page number (1‑based) from which images will be removed
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_images.pdf";
        const int    pageNumber = 2; // example: remove images from page 2

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine how many images are present on the target page
        int imageCount;
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {pageNumber}");
                return;
            }

            // Resources.Images.Count gives the number of image objects referenced by the page
            imageCount = doc.Pages[pageNumber].Resources.Images.Count;
        }

        // If there are no images, simply copy the file (or just save unchanged)
        if (imageCount == 0)
        {
            File.Copy(inputPath, outputPath, overwrite: true);
            Console.WriteLine("No images found on the specified page. File copied unchanged.");
            return;
        }

        // Build an array of image indexes (1‑based) covering all images on the page
        int[] indexes = Enumerable.Range(1, imageCount).ToArray();

        // Use PdfContentEditor (facade) to delete the images from the specified page
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);                     // load the PDF
        editor.DeleteImage(pageNumber, indexes);       // delete all images on the page
        editor.Save(outputPath);                       // save the modified PDF
        editor.Close();                                // release resources

        Console.WriteLine($"All images removed from page {pageNumber}. Output saved to '{outputPath}'.");
    }
}