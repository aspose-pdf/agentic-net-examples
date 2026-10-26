using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "wordcount_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the link annotation will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // JavaScript that calculates total word count across all pages
            string js = @"
                var total = 0;
                for (var i = 0; i < this.numPages; i++) {
                    total += this.getPageNumWords(i + 1);
                }
                app.alert('Document word count: ' + total);
            ";

            // Create the link annotation
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Action = new JavascriptAction(js),
                Color = Aspose.Pdf.Color.Blue // annotation color (border color is set via the annotation itself)
            };

            // Border must be created with the parent annotation instance
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with word‑count link saved to '{outputPath}'.");
    }
}
