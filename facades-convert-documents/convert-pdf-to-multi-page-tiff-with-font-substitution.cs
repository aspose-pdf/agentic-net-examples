using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class PdfToTiffConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        // Verify source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Font substitution: replace Symbol font with Arial Unicode MS
        // ------------------------------------------------------------
        // The PdfConverter class does not expose a FontSubstitution property.
        // Use the global FontRepository.Substitutions collection with a
        // SimpleFontSubstitution entry instead.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

        // Convert PDF to TIFF
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdfPath);

            // (Optional) set rendering options, e.g., resolution
            // converter.RenderingOptions = new RenderingOptions { Resolution = 300 };

            // Ensure all pages are processed (StartPage/EndPage are optional
            // but shown here for completeness).
            converter.StartPage = 1;
            converter.EndPage = converter.PageCount; // converts all pages

            // Perform the conversion
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiffPath);
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiffPath}");
    }
}
