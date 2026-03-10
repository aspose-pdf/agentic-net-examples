using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string svgDir    = "SvgOutput";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(svgDir);

        // -------------------------------------------------
        // 1. Load document, add a square annotation, save PDF
        // -------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Choose first page for demonstration
            Page page = doc.Pages[1];

            // Define rectangle for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create and configure a square annotation
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color   = Aspose.Pdf.Color.Red,
                Contents = "Sample square annotation",
                Title    = "Demo"
            };

            // Add annotation to the page (rotation handling not required here)
            page.Annotations.Add(square);

            // Save the document with the new annotation
            doc.Save(outputPdf);
        }

        // -------------------------------------------------
        // 2. Extract SVG representation of the page (includes annotation)
        // -------------------------------------------------
        using (Document doc = new Document(outputPdf))
        {
            Page page = doc.Pages[1];
            SvgExtractor extractor = new SvgExtractor();

            // Extract all SVG images from the page (including vector graphics)
            var svgStrings = extractor.Extract(page);

            // Save each SVG string to a separate file
            for (int i = 0; i < svgStrings.Count; i++)
            {
                string svgPath = Path.Combine(svgDir, $"page_{page.Number}_element_{i + 1}.svg");
                File.WriteAllText(svgPath, svgStrings[i]);
                Console.WriteLine($"Saved SVG: {svgPath}");
            }
        }

        // -------------------------------------------------
        // 3. Delete the previously added annotation and save again
        // -------------------------------------------------
        using (Document doc = new Document(outputPdf))
        {
            Page page = doc.Pages[1];

            // Annotations collection uses 1‑based indexing
            if (page.Annotations.Count > 0)
            {
                // Delete the first annotation (the one we added)
                page.Annotations.Delete(1);
                Console.WriteLine("Annotation deleted.");
            }

            // Save the document after deletion
            string afterDeletePdf = "output_after_delete.pdf";
            doc.Save(afterDeletePdf);
        }

        // -------------------------------------------------
        // 4. Get (list) remaining annotations on the page
        // -------------------------------------------------
        using (Document doc = new Document("output_after_delete.pdf"))
        {
            Page page = doc.Pages[1];
            Console.WriteLine($"Annotations on page {page.Number}: {page.Annotations.Count}");

            for (int i = 1; i <= page.Annotations.Count; i++)
            {
                Annotation ann = page.Annotations[i];
                Console.WriteLine($"  [{i}] Type={ann.AnnotationType}, Title={ (ann is MarkupAnnotation ma ? ma.Title : "N/A") }, Contents={ann.Contents}");
            }
        }
    }
}