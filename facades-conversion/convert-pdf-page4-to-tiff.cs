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
        const string outputTiff = "page4.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PDF converter
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(doc);

            // Limit conversion to page 4 only
            converter.StartPage = 4;
            converter.EndPage   = 4;

            // Set the resolution (higher = better quality)
            converter.Resolution = new Resolution(300);

            // Perform the conversion
            converter.DoConvert();

            // Save the selected page(s) as a TIFF image
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"Page 4 converted to TIFF: {outputTiff}");
    }
}
