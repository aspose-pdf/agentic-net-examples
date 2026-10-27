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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only Highlight annotations
                    if (annot is HighlightAnnotation highlight)
                    {
                        // Define a custom shade (e.g., orange) using Aspose.Pdf.Color
                        Aspose.Pdf.Color customColor = Aspose.Pdf.Color.FromRgb(1.0, 0.5, 0.0);

                        // Replace the annotation's color – this updates its appearance stream
                        highlight.Color = customColor;

                        // No further manipulation of the appearance dictionary is required.
                        // Setting the Color property forces Aspose.Pdf to regenerate the
                        // appearance stream automatically.
                    }
                }
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
