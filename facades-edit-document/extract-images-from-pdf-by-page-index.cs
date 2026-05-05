using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor is a Facade class; it implements IDisposable
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Use a Document only to obtain the total page count
            int pageCount;
            using (Document doc = new Document(inputPdf))
            {
                pageCount = doc.Pages.Count; // 1‑based indexing
            }

            // Iterate through each page and extract its images
            for (int page = 1; page <= pageCount; page++)
            {
                // Restrict extraction to the current page
                extractor.StartPage = page;
                extractor.EndPage   = page;

                // Perform the image extraction for this page
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve all images found on the current page
                while (extractor.HasNextImage())
                {
                    // Build the output file name using the required pattern
                    string outputPath = Path.Combine(
                        outputDir,
                        $"Image_Page{page}_Index{imageIndex}.png");

                    // Save the image in PNG format
                    extractor.GetNextImage(outputPath, ImageFormat.Png);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All images have been extracted successfully.");
    }
}
