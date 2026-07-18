using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

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

        // Step 1: Load the source PDF and apply font substitution (Symbol → Arial)
        // The substitution is achieved by specifying a default font name.
        // When a font is missing (e.g., Symbol), Aspose.Pdf will replace it with the default font.
        string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        try
        {
            using (Document srcDoc = new Document(inputPdfPath))
            {
                PdfSaveOptions saveOpts = new PdfSaveOptions
                {
                    // Use Arial as the fallback font for missing symbols.
                    DefaultFontName = "Arial"
                };

                // Save the PDF with the substitution applied.
                srcDoc.Save(tempPdfPath, saveOpts);
            }

            // Step 2: Convert the substituted PDF to a single multi‑page TIFF image.
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the temporary PDF that already contains the font substitution.
                converter.BindPdf(tempPdfPath);
                converter.DoConvert();

                // Save all pages as one TIFF file.
                converter.SaveAsTIFF(outputTiffPath);
            }

            Console.WriteLine($"PDF successfully converted to TIFF: {outputTiffPath}");
        }
        finally
        {
            // Clean up the temporary PDF file.
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}