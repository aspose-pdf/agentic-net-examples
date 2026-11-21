using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ZoomedPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the document to know how many pages it has.
        using (var doc = new Document(inputPdf))
        {
            // Determine the odd‑numbered pages (1‑based indexing).
            int[] oddPages = Enumerable.Range(1, doc.Pages.Count)
                                       .Where(p => p % 2 == 1)
                                       .ToArray();

            // Apply a 1.2 (120 %) zoom factor to the selected pages.
            using (var editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPdf);
                editor.ProcessPages = oddPages;   // target odd pages only
                editor.Zoom = 1.2f;               // 120 % magnification

                string zoomedPdfPath = Path.Combine(outputDir, "ZoomedOddPages.pdf");
                editor.Save(zoomedPdfPath);
                Console.WriteLine($"Saved zoom‑adjusted PDF to {zoomedPdfPath}");
            }
        }
    }
}
