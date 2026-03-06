using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for HtmlSaveOptions

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 1. Add a TextAnnotation (sticky note) to page 1
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",
                Contents = "This is a sample annotation added via code.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page (considerRotation = true)
            page.Annotations.Add(txtAnn, true);

            // -------------------------------------------------
            // 2. Get the annotation we just added
            // -------------------------------------------------
            // AnnotationCollection uses 1‑based indexing
            int annCount = page.Annotations.Count;
            if (annCount > 0)
            {
                Annotation firstAnn = page.Annotations[1];
                Console.WriteLine($"First annotation title:   {(firstAnn is MarkupAnnotation ma ? ma.Title : "N/A")}");
                Console.WriteLine($"First annotation contents: {firstAnn.Contents}");
            }

            // -------------------------------------------------
            // 3. Delete the annotation we added (last one)
            // -------------------------------------------------
            // Delete by index (1‑based). Here we delete the last annotation.
            page.Annotations.Delete(annCount);

            // -------------------------------------------------
            // 4. Save the modified document as HTML
            // -------------------------------------------------
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Ensure full HTML is generated
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml,
                // Embed images as PNG inside SVG to keep the output self‑contained
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML file saved to '{outputHtml}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion requires GDI+ (Windows only)
                Console.WriteLine("HTML conversion is not supported on this platform.");
            }
        }
    }
}