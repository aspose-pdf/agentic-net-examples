using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_linked.pdf";
        const string url = "https://www.example.com";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the clickable area (lower‑left X,Y and upper‑right X,Y in points)
        // Use Aspose.Pdf.Rectangle for annotation bounds
        var linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a link annotation that opens the external URL
        // LinkAnnotation constructor requires the target Page and a Rectangle
        var linkAnnotation = new LinkAnnotation(doc.Pages[1], linkRect)
        {
            // Use GoToURIAction (the correct Aspose class) for external URLs
            Action = new GoToURIAction(url)
        };

        // Add the annotation to the first page (pages are 1‑based)
        doc.Pages[1].Annotations.Add(linkAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Link annotation added. Output saved to '{outputPath}'.");
    }
}
