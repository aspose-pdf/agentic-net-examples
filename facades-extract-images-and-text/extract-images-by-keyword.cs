using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";
        const string keyword = "CONFIDENTIAL";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Initialize the extractor and bind the PDF file
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
        for (int pageNum = 1; pageNum <= extractor.Document.Pages.Count; pageNum++)
        {
            // Extract text of the current page using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            absorber.Visit(extractor.Document.Pages[pageNum]);
            string pageText = absorber.Text;

            // If the page contains the keyword, extract its images
            if (!string.IsNullOrEmpty(pageText) &&
                pageText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                // Limit extraction to the current page only
                extractor.StartPage = pageNum;
                extractor.EndPage   = pageNum;

                // Perform image extraction for this page
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(
                        outputDir,
                        $"Page{pageNum}_Image{imageIndex}.png");

                    // Save the next image to the specified file
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
            }
        }

        // Release resources held by the extractor
        extractor.Close();

        Console.WriteLine("Image extraction completed.");
    }
}