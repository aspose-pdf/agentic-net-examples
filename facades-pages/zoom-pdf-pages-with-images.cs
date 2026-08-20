using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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

        // Find pages that contain at least one image
        List<int> pagesWithImages = new List<int>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);

            // Total page count is available via the underlying Document
            int pageCount = extractor.Document.Pages.Count;

            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                extractor.StartPage = pageNum;
                extractor.EndPage   = pageNum;
                extractor.ExtractImage(); // Prepare image extraction for this page

                if (extractor.HasNextImage())
                {
                    pagesWithImages.Add(pageNum);
                }
            }
        }

        // Apply zoom only to the identified pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = pagesWithImages.ToArray(); // pages to edit
            editor.Zoom = zoomFactor;                       // set desired zoom
            editor.ApplyChanges();                          // commit changes
            editor.Save(outputPath);                        // save result
        }

        Console.WriteLine($"Zoom applied to {pagesWithImages.Count} page(s). Output saved to '{outputPath}'.");
    }
}