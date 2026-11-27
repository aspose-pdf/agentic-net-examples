using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir    = "output_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable, so wrap it in a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file.
            converter.BindPdf(inputPdfPath);

            // Set the page range: first page to page 4.
            converter.StartPage = 1;
            converter.EndPage   = 4;

            // Optional: set resolution (default is 96 DPI). Adjust as needed.
            converter.Resolution = new Resolution(150);

            // Prepare the conversion.
            converter.DoConvert();

            // Iterate through the pages in the selected range and save each as a TIFF image.
            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");
                converter.GetNextImage(outputPath, ImageFormat.Tiff);
                Console.WriteLine($"Saved page {pageNumber} as TIFF to '{outputPath}'.");
                pageNumber++;
            }

            // Release resources.
            converter.Close();
        }
    }
}
