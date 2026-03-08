using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "highlighted_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (Facade) to bind the PDF and later save it.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Access the underlying Document object.
            Document doc = editor.Document;

            // Example: highlight two text ranges on page 1.
            // Define the rectangles that cover the text to be highlighted.
            // Coordinates are in points (1/72 inch) with origin at bottom‑left.
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 250, 720);
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 650, 300, 670);

            // Create HighlightAnnotation instances.
            HighlightAnnotation highlight1 = new HighlightAnnotation(doc.Pages[1], rect1)
            {
                Color = Aspose.Pdf.Color.Yellow,   // Highlight color.
                Contents = "First highlighted text"
            };

            HighlightAnnotation highlight2 = new HighlightAnnotation(doc.Pages[1], rect2)
            {
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Second highlighted text"
            };

            // Add the annotations to the page.
            doc.Pages[1].Annotations.Add(highlight1);
            doc.Pages[1].Annotations.Add(highlight2);

            // Save the modified PDF using the facade.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Highlight annotations added and saved to '{outputPdf}'.");
    }
}