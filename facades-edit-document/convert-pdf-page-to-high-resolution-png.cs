using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page7.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Convert only page 7 to a PNG with 300 DPI using PdfConverter (Facades API)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set the page range to page 7 (Aspose.Pdf uses 1‑based indexing)
                converter.StartPage = 7;
                converter.EndPage = 7;

                // Set the desired resolution (300 DPI) – requires a Resolution object
                converter.Resolution = new Resolution(300);

                // Initialize conversion
                converter.DoConvert();

                // Export the page as PNG. The overload without ImageFormat infers the format from the file extension.
                converter.GetNextImage(outputPng);
            }

            Console.WriteLine($"Page 7 saved as high‑resolution PNG: {outputPng}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
