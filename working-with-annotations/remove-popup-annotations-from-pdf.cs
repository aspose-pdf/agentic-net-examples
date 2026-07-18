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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Collect PopupAnnotation instances first (cannot modify collection during enumeration)
                List<PopupAnnotation> popups = new List<PopupAnnotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is PopupAnnotation popup)
                    {
                        popups.Add(popup);
                    }
                }

                // Delete each popup annotation while preserving its parent markup annotation
                foreach (PopupAnnotation popup in popups)
                {
                    page.Annotations.Delete(popup);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All popup annotations removed. Saved to '{outputPath}'.");
    }
}