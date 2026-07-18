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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document disposal with using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown when hovering over the link
                Contents = "Show custom modal dialog"
            };

            // JavaScript code to display a modal dialog (using app.alert)
            // JavascriptAction is the correct class for JavaScript actions
            JavascriptAction jsAction = new JavascriptAction("app.alert('Custom modal dialog');");

            // Assign the JavaScript action to the link annotation
            link.Action = jsAction;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF (using rule: document disposal with using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}