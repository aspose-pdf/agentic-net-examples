using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing the source XSL-FO file.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Paths for input XSL-FO, intermediate PDF, final PDF/X and conversion log.
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");
        string intermediatePdfPath = Path.Combine(dataDir, "intermediate.pdf");
        string pdfXPath = Path.Combine(dataDir, "output_pdfx3.pdf");
        string logPath = Path.Combine(dataDir, "conversion_log.txt");

        // Verify that the XSL-FO source file exists.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL-FO file using XslFoLoadOptions and create a PDF document.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Optional: save the intermediate PDF (useful for debugging).
            pdfDoc.Save(intermediatePdfPath);

            // Convert the PDF to PDF/X‑3 format.
            // The conversion log will contain any errors or warnings.
            pdfDoc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the final PDF/X document.
            pdfDoc.Save(pdfXPath);
        }

        Console.WriteLine($"PDF/X file saved to '{pdfXPath}'.");
    }
}