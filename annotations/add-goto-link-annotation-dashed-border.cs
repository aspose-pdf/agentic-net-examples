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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            // Adjust coordinates as needed; here we place a small box near the top‑left corner
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a LinkAnnotation with the rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set the action to go to a specific page (e.g., page 2). Use the page object directly.
            link.Action = new GoToAction(doc.Pages[2]);

            // Configure the border: dashed style with width = 1. Border requires the owning annotation.
            link.Border = new Border(link) { Width = 1, Style = BorderStyle.Dashed };

            // Optionally set a visible border color (default is black)
            link.Color = Color.Black;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPath}'.");
    }
}
