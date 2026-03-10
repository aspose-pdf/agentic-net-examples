using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class PdfToTiffConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // Path to the source PDF
        const string outputTiffPath = "output.tiff"; // Path for the resulting TIFF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        // Create a PdfConverter facade (the Facade pattern)
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded PDF to the converter
            converter.BindPdf(pdfDocument);

            // Convert all pages (page range can be adjusted if needed)
            converter.StartPage = 1;
            converter.EndPage   = pdfDocument.Pages.Count;

            // Set a high resolution to preserve image fidelity
            converter.Resolution = new Resolution(300); // 300 DPI

            // Prepare the conversion – required before calling any SaveAs* method
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file.
            // PdfConverter does not expose a Settings property; it uses internal defaults.
            // For more advanced TIFF options you would switch to TiffDevice, but the facade
            // satisfies the requirement of using the Facade pattern.
            converter.SaveAsTIFF(outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiffPath}");
    }
}
