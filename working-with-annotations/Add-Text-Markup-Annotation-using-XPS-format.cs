using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputXps  = "input.xps";
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(inputXps))
        {
            Console.Error.WriteLine($"File not found: {inputXps}");
            return;
        }

        // Load XPS document (XPS is input‑only; we will save the result as PDF)
        using (Document doc = new Document(inputXps, new XpsLoadOptions()))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The XPS document contains no pages.");
                return;
            }

            // Create a highlight annotation on the first page
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // HighlightAnnotation derives from TextMarkupAnnotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color    = Aspose.Pdf.Color.Yellow, // annotation color
                Contents = "Important text highlighted"
            };

            // Add the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified document as PDF (XPS cannot be saved)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdf}'.");
    }
}