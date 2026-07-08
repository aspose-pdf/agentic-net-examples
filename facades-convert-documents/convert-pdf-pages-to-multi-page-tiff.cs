using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        // PdfConverter is a facade that implements IDisposable, also wrap it in using
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(pdfDocument);

            // Define the page range to convert (pages are 1‑based)
            converter.StartPage = 4;
            converter.EndPage   = 9;

            // Prepare the converter
            converter.DoConvert();

            // Save the selected pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiffPath);
        }

        Console.WriteLine($"TIFF image created at '{outputTiffPath}'.");
    }
}