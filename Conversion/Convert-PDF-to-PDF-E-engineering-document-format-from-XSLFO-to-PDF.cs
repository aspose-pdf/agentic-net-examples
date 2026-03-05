using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths (replace with actual locations)
        string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");
        string pdfPath   = Path.Combine(dataDir, "output.pdf");
        string logPath   = Path.Combine(dataDir, "conversion.log");

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL-FO file into a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document doc = new Document(xslFoPath, loadOptions))
        {
            // Convert the document to PDF/E format, logging any conversion errors
            bool success = doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);
            if (!success)
            {
                Console.WriteLine("Conversion completed with warnings. See the log file for details.");
            }

            // Save the resulting PDF/E document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF/E document saved to '{pdfPath}'.");
    }
}