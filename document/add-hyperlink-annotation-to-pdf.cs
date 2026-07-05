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

        // Open the PDF – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the first page
            Page page = doc.Pages[1];

            // Define the clickable area (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation and assign a URI action
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color  = Aspose.Pdf.Color.Blue,          // optional visual cue
                Action = new GoToURIAction(url)          // opens external website
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}