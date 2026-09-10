using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for BMP
using Aspose.Pdf.Facades;   // PdfConverter resides here

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpImages";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (a Facade) to convert pages to BMP images
        // The converter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Limit conversion to pages 1 through 10
            converter.StartPage = 1;   // first page (1‑based indexing)
            converter.EndPage   = 10;  // last page to process

            // Perform any necessary initialization before extracting images
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate over each generated image until no more pages are left
            while (converter.HasNextImage())
            {
                // Build the output file name for the current page
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                // Save the current page as a BMP image
                // GetNextImage(string outputFile, ImageFormat format)
                converter.GetNextImage(outputPath, ImageFormat.Bmp);

                pageNumber++;
            }

            // Explicitly close the converter (optional, using will dispose)
            converter.Close();
        }

        Console.WriteLine("Conversion completed.");
    }
}