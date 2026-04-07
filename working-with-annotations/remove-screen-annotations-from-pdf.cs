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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Collect all ScreenAnnotation instances on the page
                    List<ScreenAnnotation> screensToRemove = new List<ScreenAnnotation>();
                    foreach (Annotation ann in page.Annotations)
                    {
                        if (ann is ScreenAnnotation screenAnn)
                        {
                            screensToRemove.Add(screenAnn);
                        }
                    }

                    // Delete each collected ScreenAnnotation
                    foreach (ScreenAnnotation screenAnn in screensToRemove)
                    {
                        page.Annotations.Delete(screenAnn);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Screen annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}