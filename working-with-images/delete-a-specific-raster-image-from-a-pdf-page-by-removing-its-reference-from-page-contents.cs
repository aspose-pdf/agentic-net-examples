using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int pageNumber = 1;   // page containing the image
        const int imageIndex = 2;   // index of the raster image to delete (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Delete the image from the page's image collection.
            // ImageDeleteAction.None removes the image from the collection
            // and also removes any references to it in the page contents.
            doc.Pages[pageNumber].Resources.Images.Delete(imageIndex, ImageDeleteAction.None);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Specified image removed and saved to '{outputPath}'.");
    }
}