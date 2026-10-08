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
        const string outputPath = "output_with_margin.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // First, enlarge each page by 5 % (white margin will be added)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                double origWidth = doc.Pages[i].PageInfo.Width;
                double origHeight = doc.Pages[i].PageInfo.Height;

                double newWidth = origWidth * 1.05;
                double newHeight = origHeight * 1.05;

                // Update the page size – this creates the extra white area
                doc.Pages[i].PageInfo.Width = newWidth;
                doc.Pages[i].PageInfo.Height = newHeight;
            }

            // Now shrink the original content so it is centred inside the larger page.
            // The content must be scaled to 1/1.05 (≈ 95 %).
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                editor.Zoom = (float)(1.0 / 1.05); // scale down content
                // Apply the zoom to every page (1‑based indexing)
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with 5% margin to '{outputPath}'.");
    }
}
