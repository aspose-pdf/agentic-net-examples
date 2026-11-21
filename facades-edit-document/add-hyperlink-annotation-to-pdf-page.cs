using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlink_added.pdf";
        const string url = "https://example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (loads the whole file; suitable for adding a single annotation)
            Document doc = new Document(inputPath);

            // Verify that the document has at least two pages (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a page 2.");
                return;
            }

            // Define the rectangle for the link annotation (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that opens the specified URL
            var link = new LinkAnnotation(doc.Pages[2], rect)
            {
                Action = new GoToURIAction(url)
            };

            // Add the annotation to page 2
            doc.Pages[2].Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);

            Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
