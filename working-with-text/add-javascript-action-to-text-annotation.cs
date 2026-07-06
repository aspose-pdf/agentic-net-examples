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

        // Open the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation which can hold an action
            Aspose.Pdf.Annotations.LinkAnnotation link = new Aspose.Pdf.Annotations.LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,          // Visual border color
                Contents = "Click to run JavaScript"    // Tooltip text
            };

            // Create the JavaScript action
            Aspose.Pdf.Annotations.JavascriptAction jsAction =
                new Aspose.Pdf.Annotations.JavascriptAction("app.alert('Hello from Aspose.Pdf!');");

            // Attach the JavaScript action to the annotation
            link.Action = jsAction;

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript annotation to '{outputPath}'.");
    }
}