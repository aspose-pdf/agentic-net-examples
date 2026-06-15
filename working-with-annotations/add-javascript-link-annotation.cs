using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the clickable area (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the LinkAnnotation first
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set visual styling after the object is instantiated
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Attach a JavaScript action that shows a modal dialog
            link.Action = new JavascriptAction("app.alert('Custom modal dialog box');");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript link saved to '{outputPath}'.");
    }
}
