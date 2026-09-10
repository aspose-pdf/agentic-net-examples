using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure font substitution: use Arial Unicode MS when a font is missing
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = "Arial Unicode MS"
            };

            // Save the PDF with the substitution settings into a memory stream
            using (MemoryStream tempPdfStream = new MemoryStream())
            {
                pdfDocument.Save(tempPdfStream, saveOptions);
                tempPdfStream.Position = 0; // rewind for reading

                // Convert the PDF (now with font substitution) to a single multi‑page TIFF
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(tempPdfStream);
                    converter.DoConvert();
                    converter.SaveAsTIFF(outputTiffPath);
                }
            }
        }

        Console.WriteLine($"PDF has been converted to TIFF: {outputTiffPath}");
    }
}