using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;                // For any text-related options (not used here)

class Program
{
    static void Main()
    {
        // Input XSL‑FO file and desired PDF/E output file
        const string xslFoPath   = "input.xslfo";
        const string pdfEPath    = "output.pdfe.pdf";

        // Verify input file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO document using XslFoLoadOptions
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();   // no external XSL stylesheet

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(xslFoPath, loadOptions))
        {
            // Convert the loaded document to PDF/E format.
            // PdfFormat.PDF_E_1 (or PDF_E_1A) represents the engineering PDF/E standard.
            // ConvertErrorAction.Delete tells the engine to drop objects it cannot convert.
            var convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1, ConvertErrorAction.Delete);
            bool conversionResult = doc.Convert(convertOptions);

            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion to PDF/E failed.");
                return;
            }

            // Save the resulting PDF/E document.
            doc.Save(pdfEPath);
        }

        Console.WriteLine($"XSL‑FO successfully converted to PDF/E: {pdfEPath}");
    }
}