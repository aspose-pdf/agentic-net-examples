using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPng = "page7.png"; // destination PNG

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfConverter implements IDisposable – use a using block.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter.
            converter.BindPdf(inputPdf);

            // Convert only page 7.
            converter.StartPage = 7;
            converter.EndPage   = 7;

            // Set the desired resolution (300 DPI).
            converter.Resolution = new Resolution(300);

            // Prepare the converter for image extraction.
            converter.DoConvert();

            // Export the selected page as a PNG image.
            // The overload without ImageFormat infers the format from the file extension.
            converter.GetNextImage(outputPng);
        }

        Console.WriteLine($"Page 7 has been saved as PNG to '{outputPng}'.");
    }
}
