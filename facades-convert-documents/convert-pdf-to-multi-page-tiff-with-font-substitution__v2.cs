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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PdfConverter with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Set a higher resolution for better image quality (optional)
                converter.Resolution = new Resolution(300);

                // Apply font substitution: replace "Courier" with "LiberationMono"
                // Font substitution is performed via FontRepository.Substitutions
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "LiberationMono"));

                // Perform the conversion preparation step
                converter.DoConvert();

                // Save all pages as a single multi‑page TIFF file
                converter.SaveAsTIFF(outputTiff);
            }
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiff}");
    }
}
