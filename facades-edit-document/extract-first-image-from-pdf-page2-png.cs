using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputImagePath = "page2_image1.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the PDF extractor from Aspose.Pdf.Facades
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(pdfPath);

            // Restrict extraction to page 2 only (pages are 1‑based)
            extractor.StartPage = 2;
            extractor.EndPage   = 2;

            // Enable image extraction
            extractor.ExtractImage();

            // Retrieve the first image on the specified page
            if (extractor.HasNextImage())
            {
                // Save the extracted image as PNG
                using (FileStream imgStream = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
                {
                    extractor.GetNextImage(imgStream);
                }

                Console.WriteLine($"First image from page 2 saved to '{outputImagePath}'.");
            }
            else
            {
                Console.WriteLine("No images found on page 2.");
            }
        }
    }
}