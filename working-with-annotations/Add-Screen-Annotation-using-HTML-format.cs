using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // required for HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputHtml = "output.html";        // HTML result
        const string mediaFile  = "sample.mp4";         // media to play in the screen annotation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Choose the page where the annotation will be placed (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle for the annotation (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                // Create the ScreenAnnotation (constructor: Page, Rectangle, media file path)
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, mediaFile)
                {
                    Title = "Sample Screen Annotation",
                    Color = Aspose.Pdf.Color.LightGray   // optional visual styling
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screenAnn);

                // Save as HTML – must pass explicit HtmlSaveOptions (otherwise a PDF is written)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed images as PNGs inside SVG to keep the output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: split each PDF page into a separate HTML file
                    SplitIntoPages = false
                };

                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML file created: {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}