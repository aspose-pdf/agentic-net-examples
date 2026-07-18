using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Define a named destination on the target page (page 2)
            // ------------------------------------------------------------
            Page targetPage = doc.Pages[2]; // 1‑based indexing
            // Add the named destination to the document's collection
            doc.NamedDestinations.Add("MyTarget", new FitExplicitDestination(targetPage));

            // ------------------------------------------------------------
            // 2. Create a link annotation on page 1 that jumps to the named destination
            // ------------------------------------------------------------
            Page sourcePage = doc.Pages[1];

            // Define the rectangle area for the link annotation (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Instantiate the annotation first
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            // Set properties that do not require the parent reference
            link.Color = Aspose.Pdf.Color.Blue;
            link.Action = new GoToAction(doc, "MyTarget");
            // Set the border after the annotation has been created (requires the parent instance)
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page
            sourcePage.Annotations.Add(link);

            // ------------------------------------------------------------
            // 3. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}