using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Set default font for missing fonts (Symbol → Arial Unicode MS)
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = "Arial Unicode MS"
            };

            // Save the document to a memory stream with the font substitution applied
            using (MemoryStream pdfStream = new MemoryStream())
            {
                pdfDoc.Save(pdfStream, saveOptions);
                pdfStream.Position = 0; // Reset stream position for reading

                // Convert the PDF (with substituted fonts) to a single multi‑page TIFF
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(pdfStream);
                    converter.DoConvert();

                    // Save all pages as one TIFF file
                    converter.SaveAsTIFF(outputTiff);
                }
            }
        }

        Console.WriteLine($"TIFF image saved to '{outputTiff}'.");
    }
}