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
        const int targetPageNumber = 3; // 1‑based page number to jump to

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Document must be disposed via using (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Validate target page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid target page number.");
                return;
            }

            // Define the clickable rectangle (coordinates in points)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);

            // Choose the page where the link will be placed (example: first page)
            Page page = doc.Pages[1];

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Color    = Aspose.Pdf.Color.Blue,                     // visual cue
                Contents = $"Go to page {targetPageNumber}"           // tooltip text
            };

            // Set the destination using an explicit destination subclass (rule: no-destination-class-use-explicit-destination)
            link.Destination = new FitExplicitDestination(doc.Pages[targetPageNumber]);

            // Attach the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF (rule: document-disposal-with-using ensures proper disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Saved to '{outputPath}'.");
    }
}