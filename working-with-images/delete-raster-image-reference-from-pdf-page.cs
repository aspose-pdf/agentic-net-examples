using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 1; // page to modify (1‑based)
        const int    imageIndex = 1; // index of the raster image in the page resources (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Remove the image reference from the page contents.
            // ImageDeleteAction.None removes the reference from the page but keeps the image object
            // in the resources collection (file size unchanged, document stays valid).
            doc.Pages[pageNumber].Resources.Images.Delete(imageIndex, ImageDeleteAction.None);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image reference removed; saved to '{outputPath}'.");
    }
}