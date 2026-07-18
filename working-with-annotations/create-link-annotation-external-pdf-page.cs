using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "source_with_link.pdf";
        const string externalPdf = "target.pdf";
        const int targetPage = 3; // 1‑based page number in the external PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(externalPdf))
        {
            Console.Error.WriteLine($"External PDF not found: {externalPdf}");
            return;
        }

        // Open the source PDF (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the link annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue,
                Contents = $"Open page {targetPage} of {Path.GetFileName(externalPdf)}"
            };

            // Set the action to open a specific page in the external PDF document
            link.Action = new GoToRemoteAction(externalPdf, targetPage);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link annotation added. Saved to '{outputPath}'.");
    }
}