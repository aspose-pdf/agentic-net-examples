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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the link annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (coordinates are in points)
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Set the JavaScript action that opens the URL in a new browser window
                Action = new JavascriptAction($"app.launchURL('{url}', true);")
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript link saved to '{outputPath}'.");
    }
}