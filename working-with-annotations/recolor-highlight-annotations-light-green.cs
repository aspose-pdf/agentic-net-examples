using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_highlight_green.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Iterate through all annotations on the page (1‑based indexing)
                    for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                    {
                        Annotation annotation = page.Annotations[annIdx];

                        // Check if the annotation is a HighlightAnnotation
                        if (annotation is HighlightAnnotation highlight)
                        {
                            // Change its color to light green
                            highlight.Color = Aspose.Pdf.Color.LightGreen;
                        }
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"All highlight annotations recolored to light green. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}