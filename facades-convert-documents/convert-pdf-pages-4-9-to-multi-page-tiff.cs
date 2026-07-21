using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputTiffPath = "output.tiff"; // resulting TIFF file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);

                // Specify the page range to convert (pages are 1‑based)
                converter.StartPage = 4;
                converter.EndPage   = 9;

                // Prepare the converter for conversion
                converter.DoConvert();

                // Save the selected pages as a single multi‑page TIFF image
                converter.SaveAsTIFF(outputTiffPath);
            }
        }

        Console.WriteLine($"TIFF image created: {outputTiffPath}");
    }
}