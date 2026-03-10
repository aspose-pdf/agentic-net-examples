using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputHtml = "linked_output.html";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the clickable rectangle area (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a LinkAnnotation on the page
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Visit Example.com"
            };

            // Set the action to open an external URL.
            // Hyperlink property cannot be assigned a string; use GoToURIAction instead.
            link.Action = new GoToURIAction("https://www.example.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Prepare HTML save options (required for non‑PDF output)
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Embed all resources into the single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Render images as PNGs embedded in the HTML
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the PDF as HTML. The HtmlSaveOptions ensures the output is HTML, not PDF.
            try
            {
                doc.Save(outputHtml, htmlOptions);
                Console.WriteLine($"HTML file with link annotation saved to '{outputHtml}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only.
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}