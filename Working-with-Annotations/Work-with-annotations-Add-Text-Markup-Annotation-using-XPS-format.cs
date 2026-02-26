using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXps = "output.xps";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Choose the page to annotate (first page in this example)
                Page page = doc.Pages[1];

                // Define the rectangle area for the annotation (coordinates in points)
                // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

                // Create a Highlight annotation (subclass of TextMarkupAnnotation)
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // Set the highlight color (semi‑transparent yellow)
                    Color = Aspose.Pdf.Color.FromRgb(1.0, 1.0, 0.0),
                    Opacity = 0.5,
                    // Optional: set a comment that appears in the annotation popup
                    Contents = "Important text highlighted."
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(highlight);

                // Save the modified document as XPS using explicit XpsSaveOptions
                XpsSaveOptions xpsOptions = new XpsSaveOptions();
                doc.Save(outputXps, xpsOptions);
            }

            Console.WriteLine($"PDF with text markup annotation saved as XPS: '{outputXps}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}