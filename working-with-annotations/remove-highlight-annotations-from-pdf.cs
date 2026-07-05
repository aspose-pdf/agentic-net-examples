using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_highlights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // AnnotationCollection uses 1‑based indexing as well.
                // Loop backwards to safely delete by index.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is HighlightAnnotation)
                    {
                        // Delete the highlight annotation at this index
                        page.Annotations.Delete(i);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotations removed. Saved to '{outputPath}'.");
    }
}