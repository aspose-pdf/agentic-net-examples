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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation and assign a URI action to open the external website
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color  = Aspose.Pdf.Color.Blue,          // optional visual cue
                Action = new GoToURIAction(url)          // opens the URL when clicked
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added and saved to '{outputPath}'.");
    }
}