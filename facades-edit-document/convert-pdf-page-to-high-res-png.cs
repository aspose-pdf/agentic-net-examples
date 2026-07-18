using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfConverter, ImageFormat (if needed)
using Aspose.Pdf.Devices;          // Resolution

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputPng = "page7.png";   // output PNG file for page 7

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfConverter (Facade) to convert the specific page to PNG
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Set the page range to only page 7 (1‑based indexing)
            converter.StartPage = 7;
            converter.EndPage   = 7;

            // Set the desired resolution (300 DPI) – requires a Resolution object
            converter.Resolution = new Resolution(300);

            // Prepare the converter for image extraction
            converter.DoConvert();

            // Extract the image if available. Use the overload that infers the format from the file extension.
            if (converter.HasNextImage())
            {
                converter.GetNextImage(outputPng); // format inferred from ".png"
                Console.WriteLine($"Page 7 saved as PNG to '{outputPng}'.");
            }
            else
            {
                Console.Error.WriteLine("No image was extracted from the PDF.");
            }
        }
    }
}
