using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "hyperlink_output.pdf";
        const string url        = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the hyperlink will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the clickable link (coordinates are in points)
            // Fully qualify Rectangle to avoid any ambiguity with System.Drawing
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a LinkAnnotation and assign a GoToURIAction to open the external website
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction(url)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}