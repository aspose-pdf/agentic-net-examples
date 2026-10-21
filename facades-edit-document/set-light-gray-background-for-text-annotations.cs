using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Load the source PDF with the core Document API
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a TextAnnotation and configure its properties
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a note with light gray background.",
                Icon     = TextIcon.Note,
                Open     = true
            };

            // The older Aspose.Pdf versions do not expose a BackgroundColor property on TextAnnotation.
            // To improve readability on dark pages we set the border (Color) to a light gray shade.
            // This gives a visual cue similar to a light‑gray background.
            textAnn.Color = Aspose.Pdf.Color.LightGray;
            textAnn.Border = new Border(textAnn)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page
            page.Annotations.Add(textAnn);

            // Save the final PDF with the annotation
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
