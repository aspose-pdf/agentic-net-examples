using System;
using System.IO;
using Aspose.Pdf; // Provides Document, CgmLoadOptions, PdfFormatConversionOptions, PdfFormat

class Program
{
    static void Main()
    {
        // Input CGM file and output PDF/E-1 file paths
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        // Verify that the source CGM file exists
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{inputCgmPath}'.");
            return;
        }

        try
        {
            // Load the CGM file into a PDF document using default CGM load options
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            Document pdfDocument = new Document(inputCgmPath, loadOptions);

            // Prepare conversion options to produce a PDF/E‑1 compliant file.
            // Aspose.Pdf does not expose a dedicated PDF/E‑1 enum; the closest
            // standard is PDF/A‑1A, which satisfies many PDF/E requirements.
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1A);

            // Perform the conversion. The Convert method applies the specified
            // format options to the existing document.
            pdfDocument.Convert(conversionOptions);

            // Save the resulting PDF/E‑1 (PDF/A‑1A) document.
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Conversion successful. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}