using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Create the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Determine even‑numbered pages (Aspose.Pdf uses 1‑based indexing)
                List<int> evenPages = new List<int>();
                for (int i = 2; i <= doc.Pages.Count; i += 2)
                {
                    evenPages.Add(i);
                }

                // Restrict editing to the even pages only
                // ProcessPages accepts an array of page numbers
                editor.ProcessPages = evenPages.ToArray();

                // Set the zoom factor to 0.8 (80%)
                editor.Zoom = 0.8f;

                // Apply the changes to the selected pages
                editor.ApplyChanges();

                // Save the modified document (lifecycle rule: use Save with explicit path)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom applied to even pages. Output saved to '{outputPath}'.");
    }
}