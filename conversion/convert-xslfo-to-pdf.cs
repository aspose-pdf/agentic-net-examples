using System;
using System.IO;
using Aspose.Pdf;

#nullable enable

class Program
{
    static void Main()
    {
        // Path to the directory containing the XSL‑FO file.
        string dataDir = @"YOUR_DATA_DIRECTORY";
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");
        string pdfPath   = Path.Combine(dataDir, "output.pdf");

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file with default options.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
    }
}