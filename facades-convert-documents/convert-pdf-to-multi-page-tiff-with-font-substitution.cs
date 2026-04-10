using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTiff = "output.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Substitute the Symbol font with Arial Unicode MS (or any installed Unicode font)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

        // Convert the PDF pages to a single multi‑page TIFF file
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPdf);
            converter.DoConvert();                     // Prepare conversion
            converter.SaveAsTIFF(outputTiff);          // Save all pages as one TIFF
        }

        Console.WriteLine($"PDF converted to TIFF: {outputTiff}");
    }
}