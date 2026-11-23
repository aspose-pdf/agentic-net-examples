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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("Document has fewer than 12 pages.");
                return;
            }

            // Page 12 (1‑based indexing)
            Page page12 = doc.Pages[12];

            // Helper to create a navigation button as a LinkAnnotation
            LinkAnnotation CreateNavButton(double llx, double lly, double urx, double ury, int targetPage)
            {
                // Define the clickable rectangle (coordinates are in points)
                var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the link annotation on page 12
                var link = new LinkAnnotation(page12, rect)
                {
                    // Visual styling – background colour of the annotation
                    Color = Aspose.Pdf.Color.LightGray
                };

                // Border must be created with the parent annotation
                link.Border = new Border(link) { Width = 1 };

                // Clamp target page to a valid range and use the page‑object constructor
                int safePage = Math.Max(1, Math.Min(targetPage, doc.Pages.Count));
                link.Action = new GoToAction(doc.Pages[safePage]);

                return link;
            }

            // Define button positions (example layout)
            double btnWidth = 50;
            double btnHeight = 20;
            double startX = 50;   // left margin
            double startY = 750;  // top margin (from bottom of page)

            // First page button
            page12.Annotations.Add(
                CreateNavButton(startX, startY, startX + btnWidth, startY - btnHeight, 1));

            // Previous page button (page 11)
            page12.Annotations.Add(
                CreateNavButton(startX + 60, startY, startX + 60 + btnWidth, startY - btnHeight, 11));

            // Next page button (page 13, ensure it exists)
            int nextPage = Math.Min(13, doc.Pages.Count);
            page12.Annotations.Add(
                CreateNavButton(startX + 120, startY, startX + 120 + btnWidth, startY - btnHeight, nextPage));

            // Last page button
            page12.Annotations.Add(
                CreateNavButton(startX + 180, startY, startX + 180 + btnWidth, startY - btnHeight, doc.Pages.Count));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Navigation buttons added and saved to '{outputPath}'.");
    }
}
