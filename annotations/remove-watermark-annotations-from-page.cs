using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage    = 17; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 17 pages
            if (doc.Pages.Count >= targetPage)
            {
                Page page = doc.Pages[targetPage];

                // Collect all WatermarkAnnotation objects on the page
                List<Annotation> watermarks = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is WatermarkAnnotation)
                        watermarks.Add(annot);
                }

                // Remove the collected watermark annotations
                foreach (Annotation wm in watermarks)
                {
                    page.Annotations.Delete(wm);
                }
            }
            else
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; page {targetPage} does not exist.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarks removed (if any) and saved to '{outputPath}'.");
    }
}