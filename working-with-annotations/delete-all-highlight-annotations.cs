using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a sample PDF that contains at least one highlight annotation.
        // This ensures the example runs in an isolated sandbox where no files
        // exist beforehand.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            Page page = seed.Pages.Add();
            // Add some text so the highlight has something to sit on.
            page.Paragraphs.Add(new TextFragment("Sample text for highlighting."));

            // Create a highlight annotation (rectangle coordinates are in points).
            HighlightAnnotation highlight = new HighlightAnnotation(page,
                new Aspose.Pdf.Rectangle(100, 700, 300, 720));
            page.Annotations.Add(highlight);

            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // Load the PDF and delete all highlight annotations.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                // Gather all HighlightAnnotation objects on the current page.
                List<HighlightAnnotation> highlights = new List<HighlightAnnotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is HighlightAnnotation ha)
                        highlights.Add(ha);
                }

                // Remove each collected highlight annotation.
                foreach (HighlightAnnotation ha in highlights)
                {
                    page.Annotations.Delete(ha);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
    }
}
