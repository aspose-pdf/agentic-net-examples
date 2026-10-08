using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "zoomed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine which pages contain at least one image
            List<int> pagesWithImages = new List<int>();
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                if (page.Resources.Images.Count > 0)
                {
                    pagesWithImages.Add(pageNum);
                }
            }

            // Apply zoom only to the identified pages using PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                if (pagesWithImages.Any())
                {
                    editor.ProcessPages = pagesWithImages.ToArray(); // 1‑based page numbers
                    editor.Zoom = 1.5f; // 150 % zoom for image pages
                }

                // Save the modified PDF (pages not in ProcessPages remain unchanged)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom applied to image pages. Output saved to '{outputPath}'.");
    }
}
