using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Ensure an input PDF exists – create a minimal one if needed.
        // ------------------------------------------------------------
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add(); // add a blank page
            placeholder.Save(inputPath);
        }

        // ------------------------------------------------------------
        // 2. Load the PDF bytes into a MemoryStream.
        // ------------------------------------------------------------
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using var memoryStream = new MemoryStream(pdfBytes);
        // Ensure the stream position is at the beginning before passing it to Document.
        memoryStream.Position = 0;

        // ------------------------------------------------------------
        // 3. Open the document from the MemoryStream.
        // ------------------------------------------------------------
        using var doc = new Document(memoryStream);

        // ------------------------------------------------------------
        // 4. Convert the document to PDF version 1.4.
        //    Document.Version is read‑only, so we use Document.Convert.
        // ------------------------------------------------------------
        string logPath = Path.Combine(Path.GetTempPath(), "pdf_version_convert_log.xml");
        doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

        // ------------------------------------------------------------
        // 5. Save the modified PDF to the file system.
        // ------------------------------------------------------------
        const string outputPath = "output.pdf";
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with version 1.4 to '{outputPath}'.");
    }
}
