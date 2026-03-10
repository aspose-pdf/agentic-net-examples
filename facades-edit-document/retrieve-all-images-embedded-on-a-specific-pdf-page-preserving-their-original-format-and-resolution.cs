using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and the page number to extract images from (1‑based indexing)
        const string pdfPath = "input.pdf";
        const int targetPage = 2; // change as needed

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (a Facade) to pull images from the specified page
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Restrict extraction to the desired page only
            extractor.StartPage = targetPage;
            extractor.EndPage   = targetPage;

            // NOTE: The ExtractImageMode property/enum is not available in the
            // current Aspose.Pdf.Facades version. The default behavior extracts
            // all images, so we simply omit this setting.
            // extractor.ExtractImageMode = ExtractImageMode.ActuallyUsed; // removed for compatibility

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images found on the page
            while (extractor.HasNextImage())
            {
                // Build a file name; the GetNextImage(string) overload saves the image
                // using its original format (e.g., .png, .jpg, .tiff, etc.).
                string outPath = Path.Combine(
                    outputFolder,
                    $"page{targetPage}_image{imageIndex}"
                );

                // Extract the image preserving its original format and resolution
                extractor.GetNextImage(outPath);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
