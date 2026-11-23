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

        using (Document doc = new Document(inputPath))
        {
            // First page
            Page page = doc.Pages[1];

            // Rectangle for the clickable area (points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // XYZ explicit destination: page 1, top‑left (0,0), zoom 150% (1.5)
            XYZExplicitDestination destination = new XYZExplicitDestination(1, 0, 0, 1.5);
            GoToAction goTo = new GoToAction(destination);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Action = goTo,
                // Border colour is set via the annotation's Color property
                Color = Aspose.Pdf.Color.Blue
            };
            // Border must be created with the parent annotation instance
            link.Border = new Border(link) { Width = 1, Style = BorderStyle.Solid };

            // Add annotation to the page and save
            page.Annotations.Add(link);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPath}'.");
    }
}
