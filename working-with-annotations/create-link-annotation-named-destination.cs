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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that will contain the link annotation (e.g., first page)
            Page sourcePage = doc.Pages[1];

            // Define the rectangle area for the link annotation
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the link annotation on the source page
            LinkAnnotation link = new LinkAnnotation(sourcePage, linkRect);
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Define the target page for the named destination (e.g., third page)
            Page targetPage = doc.Pages[3];

            // Create a named destination called "MyDestination" that points to the target page
            // The FitExplicitDestination makes the whole page visible when jumped to
            doc.NamedDestinations.Add("MyDestination", new FitExplicitDestination(targetPage));

            // Associate the link annotation with the named destination using GoToAction
            link.Action = new GoToAction(doc, "MyDestination");

            // Add the annotation to the page's annotation collection
            sourcePage.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with named destination link saved to '{outputPath}'.");
    }
}
