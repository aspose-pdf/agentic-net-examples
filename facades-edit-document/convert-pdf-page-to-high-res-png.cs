using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPng = "page7_300dpi.png";   // output PNG file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfConverter is a Facade class; wrap it in a using block for proper disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Set the page range to only page 7 (Aspose.Pdf uses 1‑based indexing)
            converter.StartPage = 7;
            converter.EndPage   = 7;

            // Set the desired resolution (300 DPI) – requires a Resolution object
            converter.Resolution = new Resolution(300);

            // Prepare the converter for image extraction
            converter.DoConvert();

            // Save the extracted page as a PNG image.
            // The overload without ImageFormat infers the format from the file extension.
            converter.GetNextImage(outputPng);
        }

        Console.WriteLine($"Page 7 saved as high‑resolution PNG: {outputPng}");
    }
}
