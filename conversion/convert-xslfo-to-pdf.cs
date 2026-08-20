using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source XSL‑FO file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input XSL‑FO file and output PDF file paths.
        string xslFoFile = Path.Combine(dataDir, "XSLFO-to-PDF.xslfo");
        string pdfFile   = Path.Combine(dataDir, "XSLFO-to-PDF.pdf");

        // Verify that the source file exists.
        if (!File.Exists(xslFoFile))
        {
            Console.Error.WriteLine($"Error: XSL‑FO file not found at '{xslFoFile}'.");
            return;
        }

        // Create load options for XSL‑FO conversion.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Load the XSL‑FO document and convert it to PDF.
        using (Document pdfDocument = new Document(xslFoFile, loadOptions))
        {
            // Save the resulting PDF file.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"XSL‑FO successfully converted to PDF: '{pdfFile}'.");
    }
}