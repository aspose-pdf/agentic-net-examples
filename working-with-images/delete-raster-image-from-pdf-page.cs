using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that contains the raster image to delete (1‑based indexing)
            Page page = doc.Pages[1];

            // Delete the first image from the page's resources.
            // ImageDeleteAction.None removes the image reference from the page contents
            // but keeps the image object in the document (file size unchanged).
            page.Resources.Images.Delete(1, ImageDeleteAction.None);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image removed and PDF saved to '{outputPath}'.");
    }
}