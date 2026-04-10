using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat for JPEG
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Facades;             // Facade classes (PdfExtractor, etc.)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document and convert page 5 to grayscale
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            const int targetPageNumber = 5;

            if (targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages.");
                return;
            }

            // Convert the specific page to grayscale
            doc.Pages[targetPageNumber].MakeGrayscale();

            // Use PdfExtractor (Facade) to extract images from the grayscale page
            using (PdfExtractor extractor = new PdfExtractor(doc))
            {
                extractor.StartPage = targetPageNumber;
                extractor.EndPage   = targetPageNumber;

                // Prepare the extractor for image extraction
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"page{targetPageNumber}_image{imageIndex}.jpg");

                    // Save each extracted image as JPEG
                    extractor.GetNextImage(outputPath, ImageFormat.Jpeg);
                    Console.WriteLine($"Saved: {outputPath}");
                    imageIndex++;
                }

                if (imageIndex == 1)
                {
                    Console.WriteLine("No images found on the specified page.");
                }
            }
        }
    }
}