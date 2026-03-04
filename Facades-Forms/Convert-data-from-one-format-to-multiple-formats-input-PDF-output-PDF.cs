using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade APIs (e.g., PdfConverter) are available here

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output files for the different PDF formats
        const string outputPdfA = "output_pdfa.pdf";
        const string outputPdfX = "output_pdfx.pdf";
        const string outputStandard = "output_standard.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Document doc = new Document(inputPdf))
            {
                // -----------------------------------------------------------------
                // 1. Save the document as a standard PDF (no conversion)
                // -----------------------------------------------------------------
                doc.Save(outputStandard);
                Console.WriteLine($"Standard PDF saved to '{outputStandard}'.");

                // -----------------------------------------------------------------
                // 2. Convert to PDF/A-1B format
                // -----------------------------------------------------------------
                // Create conversion options for PDF/A-1B with default error handling
                PdfFormatConversionOptions pdfaOptions = new PdfFormatConversionOptions(
                    PdfFormat.PDF_A_1B,               // Target format
                    ConvertErrorAction.Delete);       // Remove objects that cannot be converted

                // Perform the conversion; the method returns true on success
                bool pdfaResult = doc.Convert(pdfaOptions);
                if (pdfaResult)
                {
                    // After conversion the document is now in PDF/A format, so save it
                    doc.Save(outputPdfA);
                    Console.WriteLine($"PDF/A-1B saved to '{outputPdfA}'.");
                }
                else
                {
                    Console.Error.WriteLine("PDF/A conversion failed.");
                }

                // -----------------------------------------------------------------
                // 3. Convert to PDF/X-3 format
                // -----------------------------------------------------------------
                // Create conversion options for PDF/X-3
                PdfFormatConversionOptions pdfxOptions = new PdfFormatConversionOptions(
                    PdfFormat.PDF_X_3,                // Target format
                    ConvertErrorAction.Delete);       // Remove objects that cannot be converted

                // Perform the conversion
                bool pdfxResult = doc.Convert(pdfxOptions);
                if (pdfxResult)
                {
                    doc.Save(outputPdfX);
                    Console.WriteLine($"PDF/X-3 saved to '{outputPdfX}'.");
                }
                else
                {
                    Console.Error.WriteLine("PDF/X conversion failed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}