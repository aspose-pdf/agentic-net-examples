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
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // ----- Add a Link Annotation -----
            // Define the rectangle area for the link (coordinates in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create the link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect);

            // Example action: go to page 2 (if it exists)
            if (doc.Pages.Count >= 2)
                link.Action = new GoToAction(doc.Pages[2]);

            // Add the link annotation to the page
            page.Annotations.Add(link);

            // ----- Add an extra Text Annotation -----
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextAnnotation txtAnn = new TextAnnotation(page, textRect);
            txtAnn.Title = "Note";
            txtAnn.Contents = "This is an extra text annotation.";
            txtAnn.Open = true;
            txtAnn.Icon = TextIcon.Note;
            page.Annotations.Add(txtAnn);

            // ----- Retrieve contents of the Link Annotation -----
            // For a GoToAction we can read the destination page number by casting to a concrete destination type
            if (link.Action is GoToAction goTo)
            {
                // The Destination property is of type IAppointment; cast to a concrete explicit destination
                var xyzDest = goTo.Destination as XYZExplicitDestination;
                if (xyzDest != null)
                {
                    Console.WriteLine($"Link annotation points to page {xyzDest.PageNumber}");
                }
                else
                {
                    Console.WriteLine("Link annotation uses a non‑XYZ destination type.");
                }
            }
            else if (link.Action is GoToURIAction uri)
            {
                Console.WriteLine($"Link annotation points to URI: {uri.URI}");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
