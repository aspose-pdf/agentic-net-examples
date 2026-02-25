using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xslFoPath   = "input.xslfo";          // XSL‑FO source file
        const string outputPath  = "output_pdfx3.pdf";     // PDF/X‑3 result
        const string logPath     = "conversion_log.xml";   // optional conversion log

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO document (it becomes a PDF in memory)
        using (Document doc = new Document(xslFoPath, new XslFoLoadOptions()))
        {
            // Convert the in‑memory PDF to PDF/X‑3 format
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted PDF/X‑3 file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 file saved to '{outputPath}'.");
    }
}