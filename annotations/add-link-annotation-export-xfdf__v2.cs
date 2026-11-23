using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath      = "annotations.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // 1. Add a link annotation to the first page
            // -------------------------------------------------
            // Use 1‑based page indexing (Aspose.Pdf uses 1‑based indexes)
            Page page = doc.Pages[1];

            // Define the rectangle for the clickable area (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that opens a URL when clicked
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction("https://example.com")
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // -------------------------------------------------
            // 2. Export all link annotations to XFDF via a FileStream
            // -------------------------------------------------
            // The core API provides ExportAnnotationsToXfdf for XFDF round‑trip.
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }

            // -------------------------------------------------
            // 3. Save the modified PDF back to disk
            // -------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        Console.WriteLine($"Link annotations exported to XFDF at '{xfdfPath}'.");
    }
}
