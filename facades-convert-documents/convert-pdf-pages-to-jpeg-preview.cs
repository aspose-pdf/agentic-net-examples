using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "PreviewImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfConverter (Facade) inside a using block for deterministic disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF
            converter.BindPdf(inputPdf);

            // Set the page range for preview (pages 1 to 3)
            converter.StartPage = 1;   // minimal value is 1
            converter.EndPage   = 3;   // inclusive end page

            // Prepare the converter
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through generated images
            while (converter.HasNextImage())
            {
                // Build output file name (JPEG is the default format)
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Save the current page as a JPEG image
                converter.GetNextImage(outputPath);

                pageNumber++;
            }
        }

        Console.WriteLine("PDF preview images (pages 1‑3) have been saved as JPEG files.");
    }
}