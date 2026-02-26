using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        // Load CGM (input‑only) and convert it to a PDF document
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(inputCgm, loadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were created from the CGM file.");
                return;
            }

            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle for the markup annotation (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation (subclass of TextMarkupAnnotation)
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Important region",
                Opacity = 0.5
            };

            // Add the annotation to the page
            page.Annotations.Add(highlight);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with text markup annotation saved to '{outputPdf}'.");
    }
}