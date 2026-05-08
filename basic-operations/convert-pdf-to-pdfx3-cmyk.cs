using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;                // For any text handling (not used here)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";
        // Output PDF/X‑3 compliant file path
        const string outputPdfPath = "output_pdfx3.pdf";
        // Path to a CMYK ICC profile (ensure this file exists on the system)
        const string cmykIccProfilePath = "cmyk.icc";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(cmykIccProfilePath))
        {
            Console.Error.WriteLine($"CMYK ICC profile not found: {cmykIccProfilePath}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Configure conversion options for PDF/X‑3
                Aspose.Pdf.PdfFormatConversionOptions conversionOptions =
                    new Aspose.Pdf.PdfFormatConversionOptions(PdfFormat.PDF_X_3)
                    {
                        // Attach an OutputIntent with a CMYK ICC profile to force color space conversion
                        OutputIntent = new OutputIntent("CMYK_OutputIntent"),
                        IccProfileFileName = cmykIccProfilePath,
                        // Optional: optimize file size
                        OptimizeFileSize = true
                    };

                // Perform the conversion to PDF/X‑3 with CMYK color space
                pdfDoc.Convert(conversionOptions);

                // Save the converted document
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/X‑3 compliant file saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
