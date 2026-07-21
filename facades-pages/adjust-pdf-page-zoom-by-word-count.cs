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
        const string outputPath = "zoom_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Determine word count for each page
            // -----------------------------------------------------------------
            var pageWordCounts = new Dictionary<int, int>(); // page index (1‑based) -> word count

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Extract text of the current page
                TextAbsorber absorber = new TextAbsorber();
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                doc.Pages[i].Accept(absorber);

                // Simple word split (space, newline, tab)
                int wordCount = 0;
                if (!string.IsNullOrWhiteSpace(absorber.Text))
                {
                    wordCount = absorber.Text.Split(
                        new[] { ' ', '\n', '\r', '\t' },
                        StringSplitOptions.RemoveEmptyEntries).Length;
                }

                pageWordCounts[i] = wordCount;
            }

            // -----------------------------------------------------------------
            // 2. Map word count to a zoom factor
            //    Fewer words => higher zoom (more readable)
            // -----------------------------------------------------------------
            const float highZoom   = 1.5f; // for pages with very few words
            const float lowZoom    = 0.8f; // for pages with many words
            const int   lowThresh  = 50;  // <= this => highZoom
            const int   highThresh = 500; // >= this => lowZoom

            // Group pages by the zoom factor they need
            var zoomGroups = new Dictionary<float, List<int>>();

            foreach (var kvp in pageWordCounts)
            {
                int pageNum   = kvp.Key;
                int wordCount = kvp.Value;

                float zoom;
                if (wordCount <= lowThresh)
                    zoom = highZoom;
                else if (wordCount >= highThresh)
                    zoom = lowZoom;
                else
                    zoom = 1.0f; // default zoom for medium‑density pages

                if (!zoomGroups.ContainsKey(zoom))
                    zoomGroups[zoom] = new List<int>();

                zoomGroups[zoom].Add(pageNum);
            }

            // -----------------------------------------------------------------
            // 3. Apply zoom per page using PdfPageEditor (Facades API)
            // -----------------------------------------------------------------
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the editor to the already loaded Document instance
                editor.BindPdf(doc);

                // Apply each zoom group separately
                foreach (var group in zoomGroups)
                {
                    editor.ProcessPages = group.Value.ToArray(); // pages to edit (1‑based)
                    editor.Zoom = group.Key;                     // zoom coefficient
                    editor.ApplyChanges();                       // commit changes
                }

                // Save the modified document
                editor.Save(outputPath);
            }

            Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
        }
    }
}