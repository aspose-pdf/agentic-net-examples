using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputTiff = "output.tiff"; // resulting multi‑page TIFF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal (document‑disposal‑with‑using rule)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Apply font substitution: replace any occurrence of "Courier" with "Liberation Mono"
            // Use FontRepository.Substitutions with SimpleFontSubstitution (correct API)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "Liberation Mono"));

            // Initialise the PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Optional: configure rendering options if needed (e.g., resolution)
                // converter.RenderingOptions = new RenderingOptions { Resolution = new Resolution(300) };

                // Prepare the converter for conversion
                converter.DoConvert();

                // Convert all pages to a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
