using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output-pdfx3.pdf";
        const string logPath = "conversion_log.xml"; // optional conversion log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Convert the document in‑place to PDF/X‑3 compliance.
            // The conversion process preserves existing color profiles.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved as PDF/X-3 to '{outputPath}'.");
    }
}
