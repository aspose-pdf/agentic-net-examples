using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices; // for CompressionType

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "output.tiff";
        const int resolution = 300; // DPI for the TIFF images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Apply font substitution: replace Courier with Liberation Mono
            // Use the Substitutions collection with SimpleFontSubstitution (source, target)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "LiberationMono"));

            // Initialize the PdfConverter facade for image conversion
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Convert all pages (1‑based indexing)
                converter.StartPage = 1;
                converter.EndPage   = pdfDoc.Pages.Count;

                // Save the result as a multi‑page TIFF with the specified resolution and compression
                // The SaveAsTIFF overload expects an int for compression, so cast the enum value.
                converter.SaveAsTIFF(outputTiff, resolution, (int)CompressionType.LZW);
            }
        }

        Console.WriteLine($"PDF successfully converted to TIFF: {outputTiff}");
    }
}
