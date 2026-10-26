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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Annotations collection may be null if the page has none
                    if (page.Annotations != null)
                    {
                        foreach (Annotation annotation in page.Annotations)
                        {
                            // Process only highlight annotations
                            if (annotation is HighlightAnnotation highlight)
                            {
                                // Set opacity to 60% (0.6)
                                highlight.Opacity = 0.6f;
                            }
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All highlight annotations set to 60% opacity. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}