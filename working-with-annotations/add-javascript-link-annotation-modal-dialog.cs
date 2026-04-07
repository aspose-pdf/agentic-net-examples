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

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // JavaScript action that shows a modal dialog box
            // The script uses the built‑in app.alert method
            JavascriptAction jsAction = new JavascriptAction("app.alert('Custom modal dialog');");

            // Assign the JavaScript action to the annotation
            link.Action = jsAction;

            // Optional: set a visible border color for the annotation
            link.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}