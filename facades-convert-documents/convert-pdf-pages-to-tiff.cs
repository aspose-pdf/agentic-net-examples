using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat enum
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputFolder  = "TiffPages";          // folder for TIFF files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the PdfConverter facade
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(pdfDoc);   // bind the document
            converter.DoConvert();       // prepare for conversion

            int pageIndex = 1;
            // Iterate over each page image
            while (converter.HasNextImage())
            {
                string tiffPath = Path.Combine(outputFolder, $"page_{pageIndex}.tiff");
                // Save the current page as a TIFF image
                converter.GetNextImage(tiffPath, ImageFormat.Tiff);
                pageIndex++;
            }

            // No explicit Save needed for the converter; images are written during GetNextImage
        }

        Console.WriteLine($"PDF pages have been converted to TIFF images in '{outputFolder}'.");
    }
}