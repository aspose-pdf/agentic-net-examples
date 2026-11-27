using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTiffPath = "output.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure a font substitution rule: replace missing "Times New Roman" with "Arial".
            // Use SimpleFontSubstitution – the FontSubstitution class does not expose a 2‑argument constructor.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));

            // Initialize the PdfConverter facade with the loaded document.
            PdfConverter converter = new PdfConverter(pdfDoc);

            // Set the page range to convert (pages 1 through 3, inclusive).
            converter.StartPage = 1;
            converter.EndPage   = 3;

            // Convert the selected pages to a multi‑page TIFF image.
            converter.SaveAsTIFF(outputTiffPath);
        }

        Console.WriteLine($"Pages 1‑3 have been converted to TIFF: {outputTiffPath}");
    }
}
