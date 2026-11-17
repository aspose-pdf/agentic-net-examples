using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_highlight_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                for (int j = 0; j < page.Annotations.Count; j++)
                {
                    Annotation annot = page.Annotations[j];

                    // Process only Highlight annotations
                    if (annot is HighlightAnnotation highlight)
                    {
                        // Retrieve the appearance dictionary (keyed by state name "N", "R", "D")
                        AppearanceDictionary appearances = highlight.Appearance;

                        // Example: access the normal appearance XForm if it exists.
                        // The dictionary does NOT expose a NormalAppearance property – use the "N" key.
                        if (appearances != null && appearances.TryGetValue("N", out var normalForm))
                        {
                            // normalForm is an XForm representing the appearance stream.
                            // Custom modifications could be performed here.
                        }

                        // Replace the annotation color with a custom shade (e.g., light orange).
                        // Aspose.Pdf.Color uses 0‑1 range for RGB components.
                        highlight.Color = Color.FromRgb(1.0, 0.6, 0.0);
                    }
                }
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
