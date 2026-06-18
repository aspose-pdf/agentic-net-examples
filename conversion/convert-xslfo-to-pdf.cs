using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the XSL‑FO source file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the XSL‑FO file.
        string xslFoFile = Path.Combine(dataDir, "XSLFO-to-PDF.xslfo");

        // Desired output PDF file path.
        string pdfFile = Path.Combine(dataDir, "XSLFO-to-PDF.pdf");

        // Verify that the source file exists.
        if (!File.Exists(xslFoFile))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoFile}");
            return;
        }

        // Create load options for XSL‑FO conversion (default settings).
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Load the XSL‑FO file and convert it to a PDF document.
        using (Document pdfDocument = new Document(xslFoFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF successfully created at: {pdfFile}");
    }
}