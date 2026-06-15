using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/X‑3 compliance.
            // The conversion keeps embedded color profiles automatically.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the compliant document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 file saved to '{outputPath}'.");
    }
}
