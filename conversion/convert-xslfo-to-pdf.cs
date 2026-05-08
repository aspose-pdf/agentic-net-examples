using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the XSL‑FO source file.
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input XSL‑FO file and output PDF file paths.
        string xslFoPath = Path.Combine(dataDir, "XSLFO-to-PDF.xslfo");
        string pdfPath   = Path.Combine(dataDir, "XSLFO-to-PDF.pdf");

        // Verify that the source file exists.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Source file not found: {xslFoPath}");
            return;
        }

        try
        {
            // Initialize load options for XSL‑FO conversion.
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            // Load the XSL‑FO file and convert it to a PDF document.
            using (Document pdfDocument = new Document(xslFoPath, loadOptions))
            {
                // Save the resulting PDF.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"XSL‑FO successfully converted to PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}