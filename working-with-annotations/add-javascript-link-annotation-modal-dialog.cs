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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (change index if needed)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation on the specified rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Create a JavaScript action that shows a modal dialog
            // JavascriptAction does not have a parameter‑less constructor; pass the script string directly.
            JavascriptAction jsAction = new JavascriptAction(
                "app.alert({cMsg: 'Custom modal dialog', nIcon: 3, cTitle: 'Info'});"
            );

            // Assign the JavaScript action to the link annotation
            link.Action = jsAction;

            // Optional: make the annotation invisible (no border, no background)
            link.Border = new Border(link) { Width = 0 };
            link.Color = Aspose.Pdf.Color.Transparent;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript link annotation saved to '{outputPath}'.");
    }
}
