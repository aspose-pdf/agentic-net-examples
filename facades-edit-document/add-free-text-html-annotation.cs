using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF. Document implements IDisposable, so wrap it in a using block.
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based).
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x,y, upper‑right x,y).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Default appearance required by FreeTextAnnotation (font name, size, colour).
            var appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the page, rectangle and appearance.
            var freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Plain‑text fallback content.
                Contents = "Rich text annotation",
                // HTML content that will be rendered inside the free‑text annotation.
                RichText = "<html><body><b>Bold</b> and <i>italic</i> <u>underlined</u> text.</body></html>"
            };

            // Optional visual styling.
            freeText.Color = Aspose.Pdf.Color.LightGray; // background colour (Annotation.Color)
            freeText.Justification = Justification.Center;

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(freeText);

            // Save the modified PDF to the output file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with free‑text annotation: {outputPath}");
    }
}
