using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF
        const string outputPath = "output.pdf"; // PDF with hyperlink
        const int sourcePage = 1;                // page where the link will be placed (1‑based)
        const int targetPage = 3;                // page the link should navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the Document API (PdfFileEditor does not expose AddLink).
        Document doc = new Document(inputPath);

        // Validate page numbers.
        if (sourcePage < 1 || sourcePage > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Source page {sourcePage} is out of range (1‑{doc.Pages.Count}).");
            return;
        }
        if (targetPage < 1 || targetPage > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Target page {targetPage} is out of range (1‑{doc.Pages.Count}).");
            return;
        }

        // Define the rectangle (in points) where the hyperlink will appear.
        // Adjust these coordinates as needed for your PDF layout.
        var rect = new Rectangle(100, 500, 200, 550);

        // Create a link annotation that navigates to the target page within the same document.
        // XYZExplicitDestination requires page, left, top and zoom parameters.
        var link = new LinkAnnotation(doc.Pages[sourcePage], rect)
        {
            Action = new GoToAction(new XYZExplicitDestination(doc.Pages[targetPage], 0, 0, 1))
        };

        // Add the annotation to the source page.
        doc.Pages[sourcePage].Annotations.Add(link);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Hyperlink added on page {sourcePage} pointing to page {targetPage}.");
    }
}
