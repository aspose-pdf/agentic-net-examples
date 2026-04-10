using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
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

        // Map missing font "Courier" to "LiberationMono" for proper rendering
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "LiberationMono"));

        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPdf);

            // Set conversion resolution (Resolution object, not an int)
            converter.Resolution = new Resolution(300);

            // Perform the conversion
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputTiff);
        }

        Console.WriteLine($"TIFF image saved to '{outputTiff}'.");
    }
}
