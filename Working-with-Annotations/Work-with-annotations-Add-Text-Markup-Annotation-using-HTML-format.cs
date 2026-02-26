using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "annotated.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Choose the page to annotate (first page in this example)
                Page page = doc.Pages[1];

                // Define the rectangle where the annotation will appear
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

                // Create a HighlightAnnotation (a type of TextMarkupAnnotation)
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // Optional visual settings
                    Color = Aspose.Pdf.Color.Yellow,
                    Opacity = 0.5,
                    // Text shown in the pop‑up when the annotation is opened
                    Contents = "Important highlighted text",
                    Title = "Reviewer"
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(highlight);

                // Save as HTML – must pass HtmlSaveOptions explicitly (extension alone is ignored)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed images as PNGs inside SVG to keep the output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: split each PDF page into a separate HTML file (false = single file)
                    SplitIntoPages = false
                };

                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"Annotated HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ and is Windows‑only
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}