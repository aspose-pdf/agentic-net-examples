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
        const float zoomFactor = 1.5f; // 150% zoom for pages that contain images

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect page numbers (1‑based) that contain at least one image
            List<int> pagesWithImages = new List<int>();
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.Resources.Images.Count > 0)
                {
                    pagesWithImages.Add(i);
                }
            }

            // If no pages contain images, just save the original document
            if (pagesWithImages.Count == 0)
            {
                Console.WriteLine("No images found in the document. Saving unchanged file.");
                doc.Save(outputPath);
                return;
            }

            // Apply zoom only to the identified pages using PdfPageEditor (Facade API)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the already loaded Document instance
                editor.BindPdf(doc);

                // Specify which pages to edit
                editor.ProcessPages = pagesWithImages.ToArray();

                // Set the desired zoom coefficient (1.0 = 100%)
                editor.Zoom = zoomFactor;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to pages with images. Output saved to '{outputPath}'.");
    }
}