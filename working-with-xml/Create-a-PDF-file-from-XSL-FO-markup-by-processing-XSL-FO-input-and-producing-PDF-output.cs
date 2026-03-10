using System;
using System.IO;
using Aspose.Pdf; // Document, XslFoLoadOptions

class Program
{
    static void Main()
    {
        // Path to the directory containing the XSL‑FO file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input XSL‑FO file.
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");

        // Output PDF file.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the input file exists.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Initialize load options for XSL‑FO conversion.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Load the XSL‑FO file and convert it to a PDF document.
        using (Document pdfDocument = new Document(xslFoPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at: {pdfPath}");
    }
}