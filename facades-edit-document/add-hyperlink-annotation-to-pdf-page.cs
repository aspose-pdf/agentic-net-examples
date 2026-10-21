using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlink_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDoc = new Document(inputPath);

        // Define the clickable area on page 2 (lower‑left X/Y, upper‑right X/Y)
        // Aspose.Pdf.Rectangle uses (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a link annotation that opens the specified URL when clicked
        LinkAnnotation link = new LinkAnnotation(pdfDoc.Pages[2], linkRect)
        {
            Action = new GoToURIAction("https://example.com")
        };

        // Add the annotation to page 2 (pages are 1‑based)
        pdfDoc.Pages[2].Annotations.Add(link);

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Hyperlink added and saved to '{outputPath}'.");
    }
}