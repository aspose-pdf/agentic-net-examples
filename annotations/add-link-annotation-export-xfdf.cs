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
            // -------------------------------------------------
            // 1. Add a link annotation to the first page
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the clickable rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a link annotation that opens an external URL
            LinkAnnotation link = new LinkAnnotation(page, rect)
            {
                Action = new GoToURIAction("https://example.com")
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // -------------------------------------------------
            // 2. Export link annotations to XFDF via MemoryStream
            // -------------------------------------------------
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export annotations to XFDF. The method directly writes XFDF data to the stream.
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset stream position if you need to read the XFDF data
                xfdfStream.Position = 0;

                // Example: write XFDF to a file for verification
                File.WriteAllBytes("annotations.xfdf", xfdfStream.ToArray());
            }

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' and annotations exported to 'annotations.xfdf'.");
    }
}
