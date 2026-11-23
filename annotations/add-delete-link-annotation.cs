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
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle area for the link annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create a link annotation that points to an external URL
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new GoToURIAction("https://example.com");
            // The LinkAnnotation class does not have a Title property; use Contents for visible text
            link.Contents = "Example Link";

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Delete the annotation we just added by its index.
            // AnnotationCollection uses 1‑based indexing and provides Delete(index).
            page.Annotations.Delete(1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
