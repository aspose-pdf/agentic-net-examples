using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Prepare the conversion (uses default settings)
            converter.DoConvert();

            // Convert each page to a separate TIFF file using the default settings
            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.tiff");
                // GetNextImage writes the image to the specified path in the requested format
                converter.GetNextImage(outputPath, ImageFormat.Tiff);
                pageNumber++;
            }
        }

        Console.WriteLine($"All pages converted to TIFF images in folder: {outputFolder}");
    }
}
