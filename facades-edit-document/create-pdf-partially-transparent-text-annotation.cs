using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // 1. Create a sample PDF (in memory) with a single page.
        // ------------------------------------------------------------
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // ------------------------------------------------------------
        // 2. Add a markup annotation (Text annotation) to the page.
        // ------------------------------------------------------------
        // Rectangle(xLL, yLL, xUR, yUR) – lower‑left and upper‑right corners.
        // Coordinates are measured from the bottom‑left corner of the page.
        TextAnnotation txtAnn = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 200, 650))
        {
            Title = "Sample",
            Contents = "This annotation is 50% transparent.",
            // --------------------------------------------------------
            // 3. Set the opacity to 0.5 (50% transparent).
            // --------------------------------------------------------
            Opacity = 0.5f // Valid range: 0 (fully transparent) – 1 (fully opaque)
        };

        // Add the annotation to the page's annotations collection.
        page.Annotations.Add(txtAnn);

        // ------------------------------------------------------------
        // 4. Save the PDF to disk.
        // ------------------------------------------------------------
        doc.Save(outputPath);

        Console.WriteLine($"PDF created with a partially transparent annotation and saved to '{outputPath}'.");
    }
}