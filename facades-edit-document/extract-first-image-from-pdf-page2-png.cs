using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page2_image1.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF.
            extractor.BindPdf(inputPdf);

            // Limit extraction to page 2 only.
            extractor.StartPage = 2; // first page to extract (1‑based)
            extractor.EndPage   = 2; // last page to extract

            // Optional: enable resource‑based image extraction.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Extract images from the specified page.
            extractor.ExtractImage();

            // Save the first extracted image, if any.
            if (extractor.HasNextImage())
            {
                // GetNextImage writes the current image to the supplied file path.
                extractor.GetNextImage(outputPng);
                Console.WriteLine($"First image from page 2 saved to '{outputPng}'.");
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}
