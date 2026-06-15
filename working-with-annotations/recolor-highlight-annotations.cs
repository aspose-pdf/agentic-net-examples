using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the recommended using pattern
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing is handled by the Pages collection)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Check if the annotation is a HighlightAnnotation
                    if (ann is HighlightAnnotation highlight)
                    {
                        // Change the annotation color to light green
                        highlight.Color = Aspose.Pdf.Color.LightGreen;
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations recolored and saved to '{outputPath}'.");
    }
}