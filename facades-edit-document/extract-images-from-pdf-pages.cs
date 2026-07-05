using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // for ImageFormat

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF to obtain the total page count
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Iterate through each page and extract its images
            for (int page = 1; page <= pageCount; page++)
            {
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the same PDF file
                    extractor.BindPdf(inputPdf);

                    // Restrict extraction to the current page
                    extractor.StartPage = page;
                    extractor.EndPage   = page;

                    // Extract images from this page
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        // Build the file name: Image_Page{page}_Index{index}.png
                        string outFile = Path.Combine(
                            outputDir,
                            $"Image_Page{page}_Index{imageIndex}.png");

                        // Save the image in PNG format
                        extractor.GetNextImage(outFile, ImageFormat.Png);
                        imageIndex++;
                    }

                    // Release resources for this page
                    extractor.Close();
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}