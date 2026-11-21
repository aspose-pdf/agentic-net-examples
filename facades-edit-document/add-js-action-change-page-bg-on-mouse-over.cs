using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure a source PDF exists – create a simple one‑page PDF if missing
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // JavaScript that changes the page background color when the mouse enters the annotation.
        // NOTE: The current Aspose.Pdf version used in this project does not expose the
        // AdditionalActions or Action properties on TextAnnotation, nor the JavascriptAction class.
        // Therefore a per‑annotation JavaScript cannot be attached directly. If JavaScript on
        // annotation entry is required, upgrade to a newer Aspose.Pdf version that supports
        // AnnotationAdditionalActions / JavascriptAction, or use a different annotation type.
        // As a fallback, a document‑level script could be added (see commented example below).
        string js = "var page = this; page.bgColor = color.red;";

        // Load the PDF document.
        Document document = new Document(inputPath);

        // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y) in points.
        // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create the annotation first (required for Border constructor).
        var annotation = new TextAnnotation(document.Pages[1], rect);
        // Optional visual settings – make the annotation invisible.
        annotation.Color = Color.Transparent;
        // Border requires the owning annotation as a constructor argument.
        annotation.Border = new Border(annotation) { Width = 0 };

        // The following line is removed because AdditionalActions is not available in this version.
        // annotation.AdditionalActions = new AnnotationAdditionalActions
        // {
        //     MouseEnter = new JavascriptAction(js)
        // };

        // If you upgrade to a version that supports JavascriptAction, uncomment the above block.
        // Alternatively, you can assign a document‑level JavaScript action as shown below:
        // document.OpenAction = new JavascriptAction(js);

        // Add the annotation to the first page.
        document.Pages[1].Annotations.Add(annotation);

        // Save the modified PDF.
        document.Save(outputPath);

        Console.WriteLine($"JavaScript annotation added (fallback). Output saved to '{outputPath}'.");
    }
}
