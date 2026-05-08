using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "linked_output.pdf";
        const int   targetPage = 3;               // page to navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the target page exists
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Target page {targetPage} is out of range.");
                return;
            }

            // Choose the page where the link annotation will be placed (e.g., first page)
            Page sourcePage = doc.Pages[1];

            // Define the clickable rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue,          // visual border color
                Contents = $"Go to page {targetPage}"   // tooltip text
            };

            // Set the action to jump to the target page within the same document
            link.Action = new GoToAction(doc.Pages[targetPage]);

            // Add the annotation to the page
            sourcePage.Annotations.Add(link);

            // Save the modified PDF (using rule for document disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}