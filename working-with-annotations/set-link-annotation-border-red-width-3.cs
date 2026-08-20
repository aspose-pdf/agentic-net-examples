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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page (1‑based indexing)
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Process only LinkAnnotation objects
                    if (ann is LinkAnnotation link)
                    {
                        // Set the border color to red
                        link.Color = Aspose.Pdf.Color.Red;

                        // Create a Border object for this annotation and set line width to 3 points
                        link.Border = new Border(link) { Width = 3 };
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All link annotations updated and saved to '{outputPath}'.");
    }
}