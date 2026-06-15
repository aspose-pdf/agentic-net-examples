using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_link.pdf";
        const int   targetPage = 3;               // page to navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the target page exists
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Target page {targetPage} is out of range.");
                return;
            }

            // Choose the page where the link annotation will be placed
            Page page = doc.Pages[1]; // first page (1‑based indexing)

            // Define the rectangle area for the link annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,   // visual cue for the link
                Contents = $"Go to page {targetPage}"
            };

            // Set the action to navigate to the target page within the same document
            link.Action = new GoToAction(doc.Pages[targetPage]);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Saved to '{outputPath}'.");
    }
}