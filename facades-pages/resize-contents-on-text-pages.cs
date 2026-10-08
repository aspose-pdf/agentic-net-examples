using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";
        const double scaleFactor = 0.9; // uniform scaling factor (both X and Y)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine which pages actually contain text
            List<int> pagesWithText = new List<int>();
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages[pageNum].Accept(absorber);

                if (!string.IsNullOrEmpty(absorber.Text))
                {
                    pagesWithText.Add(pageNum);
                }
            }

            if (pagesWithText.Count > 0)
            {
                // Use PdfPageEditor to apply a uniform zoom (scale) to the selected pages
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(doc);
                    editor.ProcessPages = pagesWithText.ToArray(); // 1‑based page numbers
                    editor.Zoom = (float)scaleFactor;            // uniform scaling for X and Y
                    editor.Save(outputPath);
                }
            }
            else
            {
                // No pages contain text – just copy the original file
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
