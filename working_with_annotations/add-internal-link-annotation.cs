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

        // Load the PDF, modify it, and save it – all within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed (e.g., first page)
            Page sourcePage = doc.Pages[1];

            // Define the rectangle area of the link annotation (coordinates in points)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the chosen page
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            // Optional visual styling
            link.Color = Aspose.Pdf.Color.Blue;
            // Border must be created after the annotation instance exists
            link.Border = new Border(link) { Width = 1 };
            // Set the action to go to the target page within the same document
            link.Action = new GoToAction(doc.Pages[targetPage]);

            // Add the annotation to the page's annotation collection
            sourcePage.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}
