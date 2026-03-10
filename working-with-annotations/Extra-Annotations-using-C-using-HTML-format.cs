using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: using block for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Add a text annotation (sticky note) on the first page
                Page page = doc.Pages[1]; // page indexing is 1‑based (rule)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550); // fully qualified to avoid ambiguity
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title    = "Extra Note",
                    Contents = "This annotation was added programmatically.",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true,
                    Icon     = TextIcon.Note
                };
                page.Annotations.Add(txtAnn); // AnnotationCollection.Add (rule)

                // Save the document as HTML – must pass explicit HtmlSaveOptions (rule)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Example of using the markup generation mode enum:
                    // HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
                };

                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only)
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