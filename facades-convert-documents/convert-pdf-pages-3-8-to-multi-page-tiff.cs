using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // PdfConverter resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputTiff = "pages3to8.tiff";    // resulting TIFF file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        // PdfConverter is a Facade; it also implements IDisposable
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Specify the page range (Aspose.Pdf uses 1‑based indexing)
            converter.StartPage = 3;
            converter.EndPage   = 8;

            // Perform any necessary initialization
            converter.DoConvert();

            // Save the selected pages as a single multi‑page TIFF.
            // Default resolution (150 DPI) and default CoordinateType (CropBox) are used.
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"Pages 3‑8 have been saved to TIFF file: {outputTiff}");
    }
}