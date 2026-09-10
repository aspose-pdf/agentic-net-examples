using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed (1‑based index)
            Page sourcePage = doc.Pages[1];

            // Define the rectangle area of the link annotation (llx, lly, urx, ury)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation on the chosen page
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            // Optional visual styling – set after the object is instantiated
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Destination page number (1‑based). Here we navigate to page 2.
            int targetPageNumber = 2;
            // Use an explicit destination (Fit the whole page) as recommended by the rule set
            link.Destination = new FitExplicitDestination(doc.Pages[targetPageNumber]);

            // Add the annotation to the page's annotation collection
            sourcePage.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}
