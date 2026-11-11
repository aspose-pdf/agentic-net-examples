using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_zoomed.pdf";
        const float zoomFactor = 1.5f; // 150% zoom

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (rule: use Document within a using block)
        using (Document doc = new Document(inputPath))
        {
            // Determine which pages contain at least one image
            List<int> pagesWithImages = new List<int>();
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing (rule)
            {
                Page page = doc.Pages[i];
                // If the page has any image resources, mark it
                if (page.Resources.Images != null && page.Resources.Images.Count > 0)
                {
                    pagesWithImages.Add(i);
                }
            }

            // If no pages contain images, just copy the original file
            if (pagesWithImages.Count == 0)
            {
                doc.Save(outputPath);
                Console.WriteLine($"No image pages found. Original PDF saved to '{outputPath}'.");
                return;
            }

            // Apply zoom only to the identified pages using PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);                     // bind the original PDF
                editor.ProcessPages = pagesWithImages.ToArray(); // target pages (1‑based)
                editor.Zoom = zoomFactor;                       // set zoom factor
                editor.Save(outputPath);                        // save the modified PDF
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
