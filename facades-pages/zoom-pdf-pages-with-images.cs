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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect page numbers that contain at least one image
            List<int> pagesWithImages = new List<int>();

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Use PdfExtractor to check for images on the current page
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(doc);
                    extractor.StartPage = pageNum;
                    extractor.EndPage   = pageNum;
                    extractor.ExtractImage();

                    if (extractor.HasNextImage())
                    {
                        pagesWithImages.Add(pageNum);
                    }
                }
            }

            // If no pages contain images, simply save the original document
            if (pagesWithImages.Count == 0)
            {
                doc.Save(outputPath);
                Console.WriteLine("No image pages found. Document saved unchanged.");
                return;
            }

            // Apply zoom to the identified pages using PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Specify the pages to be edited
                editor.ProcessPages = pagesWithImages.ToArray();

                // Set desired zoom factor (e.g., 150% of original size)
                editor.Zoom = 1.5f;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Zoom applied to {pagesWithImages.Count} page(s). Saved to '{outputPath}'.");
        }
    }
}