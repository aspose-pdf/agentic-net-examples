using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfToTiffConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputTiffPath = "output.tiff"; // resulting multi‑page TIFF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the Facade converter
            PdfConverter converter = new PdfConverter();

            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Specify the page range to convert (pages are 1‑based)
            converter.StartPage = 4;
            converter.EndPage   = 9;

            // Prepare the converter (required before saving)
            converter.DoConvert();

            // Save the selected pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiffPath);
        }

        Console.WriteLine($"Pages 4‑9 of '{inputPdfPath}' have been saved to '{outputTiffPath}'.");
    }
}