using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PostScript file and output PDF/E file paths.
        string inputPsPath = "input.ps";
        string outputPdfEPath = "output.pdf";

        // Verify that the source PS file exists.
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Error: PostScript file not found at '{inputPsPath}'.");
            return;
        }

        try
        {
            // Load the PS file. Aspose.Pdf detects the format automatically.
            using (Document pdfDoc = new Document(inputPsPath))
            {
                // Convert the document to PDF/E‑1.
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);
                pdfDoc.Convert(conversionOptions);

                // Save the converted document as a regular PDF file.
                pdfDoc.Save(outputPdfEPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"Conversion completed successfully. PDF/E saved to '{outputPdfEPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
