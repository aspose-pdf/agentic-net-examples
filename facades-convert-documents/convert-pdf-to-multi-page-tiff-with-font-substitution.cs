using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Register custom font substitution: map Symbol to Arial Unicode MS
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));
        // Optional: fallback for any missing font
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial Unicode MS"));

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfConverter facade and bind the prepared document
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(doc);

                // Convert all pages to a single multi‑page TIFF file
                converter.DoConvert();
                converter.SaveAsTIFF(outputTiff);
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
