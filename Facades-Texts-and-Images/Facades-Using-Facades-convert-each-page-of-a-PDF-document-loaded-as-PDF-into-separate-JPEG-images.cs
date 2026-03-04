using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PageImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter implements IDisposable, so use a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdfPath);

                // Prepare the converter (loads pages, etc.)
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate over all pages and save each as a JPEG image
                while (converter.HasNextImage())
                {
                    string outputImagePath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                    // Save the current page as JPEG
                    converter.GetNextImage(outputImagePath, ImageFormat.Jpeg);
                    pageNumber++;
                }
            }

            Console.WriteLine($"All pages have been saved as JPEG images in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}