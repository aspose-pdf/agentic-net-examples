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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the visual link rectangle will be placed
            Page page = doc.Pages[1]; // first page (1‑based indexing)

            // Define the rectangle area for the link annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a LinkAnnotation (its subtype is /Link by default)
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set the action to go to an internal page (e.g., page 2)
            link.Action = new GoToAction(2); // target page number (1‑based)

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with internal link: '{outputPath}'.");
    }
}