using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Change PDF version to 1.5 using Convert (Version property is read‑only)
            doc.Convert(logPath, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Initial save
            doc.Save(outputPath);
        }

        // Perform incremental update: reopen with a read/write stream and save without options
        using (var fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (var doc = new Document(fs))
        {
            // No further modifications; calling Save() triggers an incremental update
            doc.Save();
        }

        Console.WriteLine($"PDF saved with version 1.5 and incremental update to '{outputPath}'.");
    }
}
