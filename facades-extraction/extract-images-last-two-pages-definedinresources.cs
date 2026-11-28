using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine total number of pages in the PDF
        int totalPages;
        using (Document doc = new Document(inputPath))
        {
            totalPages = doc.Pages.Count;
        }

        // Guard against PDFs with fewer than two pages
        if (totalPages < 2)
        {
            Console.Error.WriteLine("PDF has fewer than two pages; nothing to extract.");
            return;
        }

        // Set up the extractor to work on the last two pages
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);

            // Pages are 1‑based; select the last two pages
            extractor.StartPage = totalPages - 1;
            extractor.EndPage   = totalPages;

            // Extract only images that are defined in the page resources
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 0;

            // Loop through all extracted images
            while (true)
            {
                string outputPath = $"page_{extractor.StartPage}_image_{imageIndex}.png";

                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    // GetNextImage returns false when no more images are available
                    if (!extractor.GetNextImage(imageStream))
                        break;
                }

                Console.WriteLine($"Saved image: {outputPath}");
                imageIndex++;
            }
        }
    }
}