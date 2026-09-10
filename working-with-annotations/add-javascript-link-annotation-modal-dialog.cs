using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (coordinates are in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the specified page and rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);
            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Assign a JavaScript action that shows a modal dialog box
            // The script uses the Acrobat JavaScript API: app.alert()
            link.Action = new JavascriptAction("app.alert('This is a custom modal dialog box.');");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript link annotation to '{outputPath}'.");
    }
}
