using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_linked.pdf";
        const string url        = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Get the first page (pages are 1‑based)
        Page page = pdfDocument.Pages[1];

        // Define the clickable area (coordinates are in points, origin at bottom‑left)
        // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing
        Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

        // Create a link annotation that opens an external URL.
        // The constructor requires the owning Page and the rectangle bounds.
        LinkAnnotation link = new LinkAnnotation(page, linkRect)
        {
            Action = new GoToURIAction(url)
        };

        // Add the annotation to the page
        page.Annotations.Add(link);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Link annotation added and saved to '{outputPath}'.");
    }
}
