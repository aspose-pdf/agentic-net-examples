using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Facades namespace included as requested

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (required for LinkAnnotation constructor)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x,y, upper‑right x,y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that will execute JavaScript
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // JavaScript action to show an alert
            JavascriptAction jsAction = new JavascriptAction("app.alert('Hello from Aspose.Pdf!');");
            link.Action = jsAction;

            // Optional visual settings
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };
            link.Highlighting = HighlightingMode.Invert;

            // Add the annotation to the first page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript annotation added and saved to '{outputPath}'.");
    }
}
