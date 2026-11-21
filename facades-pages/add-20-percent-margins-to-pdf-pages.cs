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

        // Load the document to obtain the page count (no editing is done via Document)
        using (Document doc = new Document(inputPath))
        {
            // Prepare the PdfPageEditor – this class works on the bound PDF file
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Target all pages (1‑based indexing)
                int pageCount = doc.Pages.Count;
                editor.ProcessPages = Enumerable.Range(1, pageCount).ToArray();

                // 20 % margin on each side means the content should occupy 60 % of the original size
                // (100 % – 20 % left – 20 % right = 60 %). The same applies vertically.
                // Setting Zoom to 0.6 scales the page content accordingly.
                editor.Zoom = 0.6f;

                // Center the scaled content so the empty space is distributed evenly
                editor.HorizontalAlignment = HorizontalAlignment.Center;
                editor.VerticalAlignmentType = VerticalAlignment.Center;

                // Save the edited PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with 20% margins: {outputPath}");
    }
}
