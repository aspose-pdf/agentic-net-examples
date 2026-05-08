using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

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

        // Register a font substitution: replace missing "Courier" with "Liberation Mono"
        // This affects all subsequent PDF processing in the current AppDomain.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "Liberation Mono"));

        // Load the PDF document (using a using block for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a PdfConverter facade bound to the loaded document
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter
                converter.BindPdf(pdfDoc);

                // Set rendering options to use the substitute font as the default fallback
                // (useful if the PDF references fonts that are not embedded)
                converter.RenderingOptions.DefaultFontName = "Liberation Mono";

                // Perform any necessary initial conversion steps
                converter.DoConvert();

                // Save all pages as a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);
            }
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}