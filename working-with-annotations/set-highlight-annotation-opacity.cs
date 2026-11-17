using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_highlights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only highlight annotations
                    if (annot is HighlightAnnotation highlight)
                    {
                        // Set opacity to 60% (0.6). Value range is 0.0‑1.0
                        highlight.Opacity = 0.6f;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight opacity updated and saved to '{outputPath}'.");
    }
}