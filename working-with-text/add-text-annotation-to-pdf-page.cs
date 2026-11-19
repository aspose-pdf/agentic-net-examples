using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x,y and upper‑right x,y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation
            TextAnnotation txtAnnot = new TextAnnotation(page, rect)
            {
                Title = "Info",
                Contents = "Click to run script.",
                Open = true,
                Icon = TextIcon.Note
            };

            // NOTE: The current Aspose.Pdf version does not expose an Action property on TextAnnotation
            // and does not provide a JavascriptAction class for annotations. If you need a JavaScript
            // action attached to an annotation, upgrade to a newer Aspose.Pdf version that supports it
            // or assign a document‑level JavaScript action, e.g.:
            // doc.OpenAction = new JavascriptAction("app.alert('Hello from JavaScript!');");

            // Add the annotation to the page
            page.Annotations.Add(txtAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with text annotation to '{outputPath}'.");
    }
}
