using System;
using System.IO;
using Aspose.Pdf;
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set the desired resolution (300 DPI) using the correct Resolution type
                converter.Resolution = new Resolution(300);

                // Convert only page 7 (1‑based indexing)
                converter.StartPage = 7;
                converter.EndPage   = 7;

                // Perform the conversion
                converter.DoConvert();

                // Save the resulting image; format is inferred from the file extension (.png)
                converter.GetNextImage(outputPng);
            }

            Console.WriteLine($"Page 7 has been saved as a high‑resolution PNG: {outputPng}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
