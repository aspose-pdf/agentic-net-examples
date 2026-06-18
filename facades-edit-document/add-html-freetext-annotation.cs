using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color for DefaultAppearance
using Aspose.Pdf;                         // Document, DefaultAppearance, Color, Rectangle
using Aspose.Pdf.Facades;                 // PdfContentEditor
using Aspose.Pdf.Annotations;             // FreeTextAnnotation, Border

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_html_freetext.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and bind it to the PdfContentEditor facade
        using (Document doc = new Document(inputPath))
        using (PdfContentEditor editor = new PdfContentEditor(doc))
        {
            // Define the rectangle where the free‑text annotation will appear
            // Aspose.Pdf.Rectangle expects lower‑left (llx, lly) and upper‑right (urx, ury) coordinates
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a FreeTextAnnotation (allows setting RichText)
            // Use DefaultAppearance constructor (font name, size, text color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            FreeTextAnnotation freeText = new FreeTextAnnotation(
                doc.Pages[1],          // target page (1‑based)
                annotRect,            // annotation rectangle in PDF coordinates
                appearance);

            // Embed HTML content – the RichText property accepts HTML markup
            freeText.RichText = "<b>Bold text</b> and <i>italic text</i> with a <a href=\"https://www.example.com\">link</a>.";

            // Optionally set a border and background color
            freeText.Color = Aspose.Pdf.Color.LightGray;          // border color
            freeText.Border = new Border(freeText) { Width = 1 };
            freeText.Opacity = 0.9f;                               // semi‑transparent

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF via the facade
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HTML free‑text annotation: {outputPath}");
    }
}
