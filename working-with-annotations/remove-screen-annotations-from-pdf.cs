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
                // Iterate pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    var toRemove = new List<Annotation>();

                    // Collect all ScreenAnnotation instances on the page
                    foreach (Annotation ann in page.Annotations)
                    {
                        if (ann is ScreenAnnotation)
                            toRemove.Add(ann);
                    }

                    // Remove the collected annotations
                    foreach (Annotation ann in toRemove)
                    {
                        page.Annotations.Delete(ann);
                    }
                }

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All ScreenAnnotation objects removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}