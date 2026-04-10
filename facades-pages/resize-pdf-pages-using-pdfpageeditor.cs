using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        byte[] pdfBytes;

        // ------------------------------------------------------------
        // 1️⃣ Ensure we have a PDF source. If the file does not exist,
        //    create a minimal PDF in memory so the example can run
        //    without external resources.
        // ------------------------------------------------------------
        if (File.Exists(inputPath))
        {
            pdfBytes = File.ReadAllBytes(inputPath);
        }
        else
        {
            // Create a one‑page PDF on the fly
            using (var placeholderDoc = new Document())
            {
                placeholderDoc.Pages.Add(); // add a blank page
                using (var ms = new MemoryStream())
                {
                    placeholderDoc.Save(ms);
                    pdfBytes = ms.ToArray();
                }
            }
            Console.WriteLine($"'{inputPath}' not found – a placeholder PDF was generated in memory.");
        }

        // ------------------------------------------------------------
        // 2️⃣ Modify the page size using PdfPageEditor. All streams are
        //    wrapped in using blocks to guarantee disposal.
        // ------------------------------------------------------------
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var editor = new PdfPageEditor())
        {
            editor.BindPdf(inputStream);
            editor.PageSize = PageSize.A4; // set desired size (A4 in this example)
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
