using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution type

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page7.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Convert a single page to a high‑resolution PNG
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind source PDF
                converter.BindPdf(inputPdf);

                // Convert only page 7 (1‑based index)
                converter.StartPage = 7;
                converter.EndPage   = 7;

                // Set resolution to 300 DPI – requires a Resolution object
                converter.Resolution = new Resolution(300);

                // Perform conversion
                converter.DoConvert();

                // Save the image; format is inferred from the file extension (.png)
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
