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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Define a named destination somewhere in the document.
            //    Here we create a destination on page 3 that fits the whole page.
            // -----------------------------------------------------------------
            int targetPageNumber = 3; // 1‑based page index

            // Use an explicit destination that fits the page. The class lives in Aspose.Pdf.Annotations.
            FitExplicitDestination fitDest = new FitExplicitDestination(doc.Pages[targetPageNumber]);

            // Add the named destination to the document's collection
            doc.NamedDestinations.Add("MyNamedDest", fitDest);

            // -----------------------------------------------------------------
            // 2. Create a link annotation on another page that jumps to the named destination.
            // -----------------------------------------------------------------
            // Choose the page where the link will appear (e.g., page 1)
            Page linkPage = doc.Pages[1];

            // Define the rectangle area for the clickable link (llx, lly, urx, ury)
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(linkPage, linkRect)
            {
                // Set the action to go to the named destination defined earlier
                Action = new GoToAction(doc, "MyNamedDest")
            };

            // Optionally set a border or appearance (invisible border)
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page's annotation collection
            linkPage.Annotations.Add(link);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with link annotation saved to '{outputPath}'.");
    }
}
