using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "BmpPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF document
            converter.BindPdf(inputPdfPath);

            // Determine how many pages to process (max 20 or total page count)
            // PageCount is available after binding the document
            int totalPages = converter.PageCount;
            int pagesToConvert = Math.Min(20, totalPages);

            // Set the page range (1‑based indexing)
            converter.StartPage = 1;
            converter.EndPage   = pagesToConvert;

            // Set the desired resolution (150 DPI)
            converter.Resolution = new Resolution(150);

            // Prepare the conversion
            converter.DoConvert();

            // Loop through the pages and save each as a BMP image
            for (int pageNumber = 1; pageNumber <= pagesToConvert; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.bmp");
                // GetNextImage saves the current page using the file extension to infer format
                converter.GetNextImage(outputPath);
                Console.WriteLine($"Saved page {pageNumber} as BMP to '{outputPath}'.");
            }
        }
    }
}
