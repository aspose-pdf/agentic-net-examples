using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions and annotation types live here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string uri = "https://example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page; change index if needed
            Page page = doc.Pages[1];

            // Define the clickable area (lower-left x, lower-left y, upper-right x, upper-right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation first
            LinkAnnotation link = new LinkAnnotation(page, rect);
            // Assign its properties after construction
            link.Action = new GoToURIAction(uri);
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPath}'.");
    }
}
