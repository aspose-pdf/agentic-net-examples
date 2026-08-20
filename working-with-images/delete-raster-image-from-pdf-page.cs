using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the name of the image to delete
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imageName  = "Image1";   // name of the raster image resource
        const int    pageNumber = 1;          // page from which the image will be removed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Remove the image reference from the page contents.
            // ImageDeleteAction.None removes the image from the collection
            // and also removes any references to it in the page content stream.
            page.Resources.Images.Delete(imageName, ImageDeleteAction.None);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image \"{imageName}\" removed from page {pageNumber}. Saved to \"{outputPath}\".");
    }
}