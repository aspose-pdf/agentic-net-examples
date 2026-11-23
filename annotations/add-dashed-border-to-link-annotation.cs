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
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Define the rectangle area for the link annotation (coordinates in points)
            // Aspose.Pdf.Rectangle constructor: (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 200, 550);

            // Get the first page
            Page page = doc.Pages[1];

            // Create a link annotation that points to a URL
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Action = new GoToURIAction("https://example.com")
            };

            // Set the border: width = 1 point, dashed pattern [3,2]
            link.Border = new Border(link)
            {
                Width = 1,
                Dash = new Dash(new int[] { 3, 2 })
            };

            // Add the annotation to the first page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dashed link border to '{outputPath}'.");
    }
}