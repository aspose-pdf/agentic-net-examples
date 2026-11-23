using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Color (if needed)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath = "highlights.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, add a highlight annotation, export highlights to XFDF, then save the PDF.
        using (Document doc = new Document(inputPdfPath))
        {
            // -------------------------------------------------
            // 1. Add a highlight annotation on the first page.
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle (llx, lly, urx, ury) in points.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create the highlight annotation.
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Use a semi‑transparent yellow.
                Color = Aspose.Pdf.Color.FromRgb(1.0, 1.0, 0.0), // Yellow
                Opacity = 0.5,
                Title = "Sample Highlight",
                Contents = "This is a highlighted text."
            };

            // Add the annotation to the page.
            page.Annotations.Add(highlight);

            // -------------------------------------------------
            // 2. Export all highlight annotations to XFDF.
            // -------------------------------------------------
            // Export annotations directly to a stream (no extra XfdfSaveOptions class needed).
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                // Reset the stream position to read its contents.
                xfdfStream.Position = 0;

                // -------------------------------------------------
                // 3. Retrieve XFDF contents as a string (optional).
                // -------------------------------------------------
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XFDF content:");
                    Console.WriteLine(xfdfContent);
                }
            }

            // -------------------------------------------------
            // 4. Save the modified PDF.
            // -------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'. Highlights exported to '{xfdfPath}'.");
    }
}
