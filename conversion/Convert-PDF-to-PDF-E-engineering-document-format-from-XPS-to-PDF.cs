using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ConvertErrorAction enum (if needed)

class Program
{
    static void Main()
    {
        // Input XPS file path
        const string xpsPath = "input.xps";
        // Output PDF/E file path (saved as .pdf)
        const string pdfPath = "output.pdf";
        // Optional log file for conversion details
        const string logPath = "conversion_log.txt";

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        try
        {
            // Load the XPS document using XpsLoadOptions
            using (Document doc = new Document(xpsPath, new XpsLoadOptions()))
            {
                // Convert the loaded document to PDF/E (represented by PdfFormat.PDF_X_4)
                // The Convert method writes conversion errors to the specified log file.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the resulting PDF/E document
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Conversion completed. PDF/E saved to '{pdfPath}'.");
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