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
        const string url = "https://www.example.com";

        // If the source PDF does not exist, create a minimal one.
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPath);
            }
        }

        // Open the PDF, add a link annotation, and save.
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation and set its action to open an external URL.
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,
                Action = new GoToURIAction(url) // external URL action
            };

            // Add the annotation to the page.
            page.Annotations.Add(link);

            // Save the updated document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Saved to '{outputPath}'.");
    }
}