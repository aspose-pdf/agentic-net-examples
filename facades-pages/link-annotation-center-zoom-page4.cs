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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (document.Pages.Count < 4)
            {
                Console.Error.WriteLine("Document must contain at least 4 pages.");
                return;
            }

            // Define the rectangle area for the link annotation on page 1
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(document.Pages[1], linkRect);
            // Title is not supported on LinkAnnotation; use Contents for tooltip text
            link.Contents = "Click to jump to page 4 with zoom 1.2";
            link.Color = Aspose.Pdf.Color.Blue;
            // Use the non‑obsolete property for horizontal alignment
            link.TextHorizontalAlignment = HorizontalAlignment.Center;

            // Create a destination that points to the upper‑left corner of page 4 with a zoom factor of 1.2
            XYZExplicitDestination destination = XYZExplicitDestination.CreateDestinationToUpperLeftCorner(document.Pages[4], 1.2);
            link.Destination = destination;

            // Add the annotation to page 1
            document.Pages[1].Annotations.Add(link);

            // Save the modified PDF
            document.Save(outputPath);
        }

        Console.WriteLine("Link annotation added and saved to '" + outputPath + "'.");
    }
}
