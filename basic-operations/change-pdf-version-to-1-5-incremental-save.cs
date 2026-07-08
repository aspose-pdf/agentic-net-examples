using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with a read/write stream so that Save() performs an incremental update.
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(fs))
        {
            // Change the PDF version to 1.5 using Document.Convert (Version property is read‑only).
            string logPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? ".", "conversion_log.xml");
            doc.Convert(logPath, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Save incrementally – updates the same file without rewriting the whole document.
            doc.Save();
        }

        Console.WriteLine("PDF version set to 1.5 and saved with incremental update.");
    }
}
