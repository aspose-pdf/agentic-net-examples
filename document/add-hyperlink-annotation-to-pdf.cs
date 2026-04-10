using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string url        = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (page-indexing-one-based rule)
            Page page = doc.Pages[1];

            // Define the clickable rectangle area on the page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation (LinkAnnotation constructor)
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // Set the action to open an external website (hyperlink-property-is-not-a-string rule)
            link.Action = new GoToURIAction(url);

            // Optional: give the link a visible color
            link.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF (document-disposal-with-using rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}