using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_link.pdf";
        const string url        = "https://example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation and associate it with the rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set the action to open an external URL
            link.Action = new GoToURIAction(url);

            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;               // Blue border
            link.Border = new Border(link) { Width = 1 };      // 1‑point border

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}