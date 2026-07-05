using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF file
        const string outputFolder = "ExtractedImages"; // folder for extracted images
        int pageNumber = 3;                           // page to extract images from

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facade) to extract images from a single page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Limit the extraction range to the desired page (StartPage == EndPage)
            extractor.StartPage = pageNumber;
            extractor.EndPage   = pageNumber;

            // Perform the image extraction for the defined page range
            extractor.ExtractImage();

            int imageIndex = 1;
            // Retrieve each extracted image and save it to a file
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(
                    outputFolder,
                    $"page{pageNumber}_image{imageIndex}.jpg"); // default format is JPEG

                // GetNextImage returns a bool indicating success; we ignore it here
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}