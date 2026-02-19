using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and ICC profile.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Paths for input PDF, external ICC profile, and output PDF/X‑4 file.
        string inputPdfPath = Path.Combine(dataDir, "input.pdf");
        string iccProfilePath = Path.Combine(dataDir, "profile.icc");
        string outputPdfPath = Path.Combine(dataDir, "output_pdfx4.pdf");

        // Verify that required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the source PDF document.
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Create conversion options for PDF/X‑4 format.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                // Assign the external ICC profile.
                options.IccProfileFileName = iccProfilePath;

                // Convert the document to PDF/X‑4 using the options.
                pdfDocument.Convert(options);

                // Save the converted document. SaveFormat.Pdf is appropriate after conversion.
                pdfDocument.Save(outputPdfPath, SaveFormat.Pdf);
            }

            Console.WriteLine($"Conversion succeeded. PDF/X‑4 saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
