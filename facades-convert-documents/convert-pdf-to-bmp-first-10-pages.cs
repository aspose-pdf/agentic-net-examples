using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where BMP images will be saved
        const string outputFolder = "BmpImages";
        Directory.CreateDirectory(outputFolder);

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (Facade) to convert pages to BMP images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Set the page range: start at page 1, end at page 10
            converter.StartPage = 1;   // minimal value is 1
            converter.EndPage   = 10;  // limit conversion to first ten pages

            // Optional: set a higher resolution for clearer images
            // The Resolution class is not available in the current Aspose.Pdf version,
            // and the property is optional, so we omit it.
            // converter.Resolution = new Aspose.Pdf.Facades.Resolution(300);

            // Perform any required initialization
            converter.DoConvert();

            int pageIndex = 1;
            // Loop through the pages in the specified range
            while (converter.HasNextImage())
            {
                // Build the output file name
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.bmp");

                // Save the current page as a BMP image
                converter.GetNextImage(outputPath, ImageFormat.Bmp);

                pageIndex++;
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("Conversion to BMP completed.");
    }
}
