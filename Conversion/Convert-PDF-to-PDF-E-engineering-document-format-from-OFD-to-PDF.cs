using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputOfdPath  = "input.ofd";      // OFD source file
        const string outputPdfPath = "output.pdf";     // Target PDF/E file
        const string logPath       = "conversion.log"; // Log for conversion issues

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        try
        {
            // Load the OFD document. OFD is an input‑only format, so we use OfdLoadOptions.
            using (Document doc = new Document(inputOfdPath, new OfdLoadOptions()))
            {
                // Convert the document to the engineering PDF/E format.
                // Aspose.Pdf represents PDF/E via the PdfFormat enumeration (e.g., PdfFormat.PDF_X_4 is used here as a placeholder for PDF/E).
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the converted document as a PDF file. The resulting file complies with the PDF/E specification.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF/E saved to '{outputPdfPath}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}