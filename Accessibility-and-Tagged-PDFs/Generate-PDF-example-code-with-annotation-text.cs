using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Get the first page (pages are 1‑based)
        Page page = pdfDocument.Pages[1];

        // Define the rectangle where the annotation will appear
        // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
        var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

        // Create a TextAnnotation (sticky note) on the specified page
        TextAnnotation textAnnot = new TextAnnotation(page, rect);
        textAnnot.Title = "Sample Title";          // Title shown in the annotation window
        textAnnot.Contents = "This is a text annotation added via Aspose.Pdf.";
        textAnnot.Color = Color.Yellow;            // Background color of the annotation

        // Initialize the border using the provided rule (cannot reference the variable inside its own initializer)
        textAnnot.Border = new Border(textAnnot)
        {
            Style = BorderStyle.Solid,
            Width = 1
        };

        // Add the annotation to the page's annotation collection
        page.Annotations.Add(textAnnot);

        // Save the modified PDF document
        pdfDocument.Save(outputPath);
    }
}