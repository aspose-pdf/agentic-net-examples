using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js_link.pdf";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect);
            // Optional visual appearance
            link.Color = Aspose.Pdf.Color.Blue;
            // Border must be set after the annotation instance exists
            link.Border = new Border(link) { Width = 1 };
            // Assign a JavaScript action that opens the URL in a new browser window
            link.Action = new JavascriptAction($"app.launchURL('{url}', true);");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript link: '{outputPath}'.");
    }
}
