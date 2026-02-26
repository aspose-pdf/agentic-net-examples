using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Add a text (sticky note) annotation to the first page
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation textAnno = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is an accessible annotation added via Aspose.Pdf.",
                Open     = true,
                Icon     = TextIcon.Note,
                // Example of setting a cross‑platform color
                Color    = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnno);

            // Prepare HTML save options (required for HTML output)
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed images as PNGs inside SVG to keep the HTML self‑contained
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                // Optional: split each PDF page into a separate HTML file
                // SplitIntoPages = true
            };

            // HTML conversion relies on GDI+ and is Windows‑only; handle possible exceptions
            try
            {
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML file saved to '{outputHtml}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML save: {ex.Message}");
            }
        }
    }
}