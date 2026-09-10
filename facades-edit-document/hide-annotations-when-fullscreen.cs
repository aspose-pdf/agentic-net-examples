using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // 1. Ensure a source PDF exists – create a minimal one if needed
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            // Create a simple PDF with one page, a text annotation and
            // Full‑Screen viewer preference set.
            using (var seed = new Document())
            {
                // Add a page
                Page page = seed.Pages.Add();

                // Add a text annotation (so we have something to hide later)
                var txtAnn = new TextAnnotation(page, new Rectangle(100, 600, 300, 650))
                {
                    Title = "Sample",
                    Contents = "This annotation will be hidden when FullScreen is on."
                };
                page.Annotations.Add(txtAnn);

                // Set viewer preference to FullScreen (use Document.PageMode)
                seed.PageMode = PageMode.FullScreen;

                // Save the seed PDF
                seed.Save(inputPath);
            }
        }

        // ------------------------------------------------------------
        // 2. Load the PDF and inspect viewer preferences
        // ------------------------------------------------------------
        using (var doc = new Document(inputPath))
        {
            bool isFullScreen = doc.PageMode == PageMode.FullScreen;

            if (isFullScreen)
            {
                // ------------------------------------------------
                // 3. Full‑Screen is enabled – hide all annotations
                // ------------------------------------------------
                foreach (Page page in doc.Pages)
                {
                    // Annotations collection is 1‑based in Aspose.Pdf
                    for (int i = 1; i <= page.Annotations.Count; i++)
                    {
                        Annotation ann = page.Annotations[i];
                        ann.Flags = ann.Flags | AnnotationFlags.Hidden;
                    }
                }
            }

            // ------------------------------------------------
            // 4. Save the (potentially modified) PDF to the output file
            // ------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
