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
        const string outputPath = "output_no_popups.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Collect all PopupAnnotation instances on the current page
                List<Annotation> popups = new List<Annotation>();
                foreach (Annotation annot in page.Annotations)
                {
                    // PopupAnnotation derives from Annotation; check its exact type
                    if (annot is PopupAnnotation)
                        popups.Add(annot);
                }

                // Remove the collected popup annotations from the page
                foreach (Annotation popup in popups)
                {
                    page.Annotations.Delete(popup);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Popup annotations removed. Saved to '{outputPath}'.");
    }
}