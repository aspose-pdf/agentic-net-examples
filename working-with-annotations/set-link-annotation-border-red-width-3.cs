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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Process only link annotations
                    if (annot is LinkAnnotation link)
                    {
                        // Set border colour (Color property controls the border colour)
                        link.Color = Color.Red;

                        // Set border line width via the Border object (requires the parent annotation)
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
