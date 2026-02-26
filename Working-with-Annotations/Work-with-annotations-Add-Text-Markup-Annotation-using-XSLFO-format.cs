using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xslFoPath = "input.xslfo";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL-FO file not found: {xslFoPath}");
            return;
        }

        // Load XSL‑FO and generate a PDF document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Generated PDF contains no pages.");
                return;
            }

            // Define the rectangle (in points) where the markup will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a highlight (text markup) annotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(pdfDoc.Pages[1], rect);
            highlight.Color = Aspose.Pdf.Color.Yellow;          // visual highlight color
            highlight.Contents = "Highlighted important text"; // optional tooltip

            // Attach the annotation to the page
            pdfDoc.Pages[1].Annotations.Add(highlight);

            // Save the PDF with the added annotation
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with text markup annotation saved to '{outputPdf}'.");
    }
}