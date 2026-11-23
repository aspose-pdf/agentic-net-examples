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
            // Choose the page to add the annotation (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the clickable link (coordinates in points)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create a LinkAnnotation that points to a mailto URI
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new GoToURIAction("mailto:example@example.com");

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPath}'.");
    }
}