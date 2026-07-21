using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";               // source PDF
        const string outputDir  = "ExtractedImages";         // folder for images
        const string keyword    = "YOUR_KEYWORD_HERE";       // text to search for

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the document once for text searching
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the extractor once and bind the same PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Search the current page for the keyword
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
                    absorber.Visit(doc.Pages[pageNum]);

                    // If the keyword is found, extract images from this page only
                    if (absorber.TextFragments.Count > 0)
                    {
                        // Restrict extraction to the current page
                        extractor.StartPage = pageNum;
                        extractor.EndPage   = pageNum;

                        // Perform image extraction for the defined range
                        extractor.ExtractImage();

                        int imageIndex = 1;
                        // Retrieve all images found on this page
                        while (extractor.HasNextImage())
                        {
                            string imagePath = Path.Combine(
                                outputDir,
                                $"page{pageNum}_img{imageIndex}.png");

                            // Save the image; the method preserves the original format
                            extractor.GetNextImage(imagePath);
                            imageIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}