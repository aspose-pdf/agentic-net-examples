using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, XslFoLoadOptions)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xslFoPath = Path.Combine(dataDir, "template.xslfo"); // XSL‑FO file
        string pdfPath   = Path.Combine(dataDir, "output.pdf");    // Result PDF

        // Ensure the XSL‑FO source exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO using the dedicated load options
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Create the Document from the XSL‑FO source (lifecycle rule: use constructor with load options)
        using (Document pdfDocument = new Document(xslFoPath, loadOptions))
        {
            // Save the generated PDF (lifecycle rule: use Document.Save(string))
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF created successfully at: {pdfPath}");
    }
}