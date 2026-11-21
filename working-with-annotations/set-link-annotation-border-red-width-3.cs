using System;
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Examine each annotation on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only Link annotations
                    if (annot is LinkAnnotation link)
                    {
                        // Set border color to red (Color property controls the border colour for link annotations)
                        link.Color = Aspose.Pdf.Color.Red;

                        // Increase border line width to three points – Border requires the owning annotation in its constructor
                        link.Border = new Border(link) { Width = 3 };
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation borders updated and saved to '{outputPath}'.");
    }
}
