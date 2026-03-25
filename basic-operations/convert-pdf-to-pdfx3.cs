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
            // Convert to PDF/X-3. The conversion process preserves embedded ICC profiles by default.
            doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully converted to PDF/X-3: {outputPath}");
    }
}