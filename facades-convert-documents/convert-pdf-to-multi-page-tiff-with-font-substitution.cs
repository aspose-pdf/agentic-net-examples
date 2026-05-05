using System;
using System.IO;
using Aspose.Pdf;
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Register a custom font substitution: replace missing Symbol font with Arial Unicode MS.
        // This substitution is applied globally for the current AppDomain.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

        // Convert PDF pages to a single multi‑page TIFF file.
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(inputPdf);
            converter.DoConvert();
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"TIFF image saved to '{outputTiff}'.");
    }
}