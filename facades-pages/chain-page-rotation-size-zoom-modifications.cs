using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------------------------
            // 1️⃣ Apply per‑page transformations that are part of the Document API:
            //    • Rotation (using the Rotation enum)
            //    • Page size (using PageInfo.Width / Height)
            // -------------------------------------------------------------------
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Rotate the page 90° clockwise
                doc.Pages[i].Rotate = Rotation.on90;

                // Set a new page size – A5 (420 × 595 points)
                doc.Pages[i].PageInfo.Width = 420;
                doc.Pages[i].PageInfo.Height = 595;
            }

            // -------------------------------------------------------------------
            // 2️⃣ Apply viewer‑zoom using PdfPageEditor (the Zoom property works on the
            //    pages specified via the ProcessPages array). This must be done after
            //    the Document modifications because PdfPageEditor works on the bound
            //    Document instance.
            // -------------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                // Target all pages (1‑based indexing)
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
                // 150 % zoom (1.5 ×)
                editor.Zoom = 1.5f;
                // Save the final PDF with all changes applied
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
