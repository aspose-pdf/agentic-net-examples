using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade with the loaded document
            PdfConverter converter = new PdfConverter(pdfDoc);

            // Set the page range (Aspose.Pdf uses 1‑based indexing)
            converter.StartPage = 4;
            converter.EndPage = 9;

            // Prepare the converter
            converter.DoConvert();

            // Convert the selected pages to a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"TIFF image created at: {outputTiff}");
    }
}