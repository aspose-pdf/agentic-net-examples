using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hyperlink.pdf";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the hyperlink will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (coordinates in points)
            // lower-left x, lower-left y, upper-right x, upper-right y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a link annotation with the defined rectangle
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set the action to open an external website
            link.Action = new GoToURIAction(url);

            // Make the annotation invisible (no visible border) by setting border width to 0
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}
