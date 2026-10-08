using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution struct lives here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputImage = "page4.tiff"; // PdfConverter supports TIFF output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Render a single page to an image using PdfConverter.
        // PdfViewer does not expose PageNumber, HorizontalAlignment or Zoom in older
        // Aspose.Pdf versions, so PdfConverter is the reliable alternative.
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document.
            converter.BindPdf(inputPdf);

            // Specify the page range – we need only page 4.
            converter.StartPage = 4;
            converter.EndPage   = 4;

            // "Zoom" is achieved by increasing the resolution. The default screen DPI is 96.
            // A 1.2× zoom corresponds to roughly 115 DPI.
            converter.Resolution = new Resolution(115);

            // Perform the conversion.
            converter.DoConvert();

            // Save the rendered page as TIFF (PdfConverter does not provide SaveAsPNG).
            converter.SaveAsTIFF(outputImage);
        }

        Console.WriteLine($"Page 4 rendered to '{outputImage}' with increased resolution.");
    }
}
