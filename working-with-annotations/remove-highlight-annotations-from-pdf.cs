using System;
using System.IO;
using System.Collections.Generic;
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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Collect highlight annotations first to avoid modifying the collection during iteration
                    List<Annotation> highlights = new List<Annotation>();
                    foreach (Annotation annot in page.Annotations)
                    {
                        if (annot is HighlightAnnotation)
                            highlights.Add(annot);
                    }

                    // Remove each collected highlight annotation
                    foreach (Annotation highlight in highlights)
                        page.Annotations.Delete(highlight);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}