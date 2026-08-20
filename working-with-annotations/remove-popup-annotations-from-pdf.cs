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

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Gather all PopupAnnotation objects on the current page
                var popups = new System.Collections.Generic.List<PopupAnnotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is PopupAnnotation popup)
                    {
                        popups.Add(popup);
                    }
                }

                // Remove each popup annotation; its parent markup annotation stays intact
                foreach (var popup in popups)
                {
                    page.Annotations.Delete(popup);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All popup annotations removed. Output saved to '{outputPath}'.");
    }
}