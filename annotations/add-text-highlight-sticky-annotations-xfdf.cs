using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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
            // -----------------------------------------------------------------
            // 1. Add plain text to the first page
            // -----------------------------------------------------------------
            // Pages are 1‑based in Aspose.Pdf
            Page page = doc.Pages[1];

            TextFragment tf = new TextFragment("Added sample text");
            tf.Position = new Position(100, 700);
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page.Paragraphs.Add(tf);

            // -----------------------------------------------------------------
            // 2. Add a highlight annotation
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect);
            highlight.Color = Aspose.Pdf.Color.Yellow; // highlight color
            page.Annotations.Add(highlight);

            // -----------------------------------------------------------------
            // 3. Add a sticky note annotation (TextAnnotation in current API)
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle stickyRect = new Aspose.Pdf.Rectangle(300, 500, 320, 520);
            TextAnnotation sticky = new TextAnnotation(page, stickyRect);
            sticky.Icon = TextIcon.Comment; // standard comment icon
            sticky.Contents = "This is a sticky note.";
            page.Annotations.Add(sticky);

            // -----------------------------------------------------------------
            // 4. Export all annotations to XFDF via a MemoryStream
            // -----------------------------------------------------------------
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Directly export annotations to XFDF – no XfdfSaveOptions class needed
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Example: write XFDF to a file (optional)
                // File.WriteAllBytes("annotations.xfdf", xfdfStream.ToArray());
            }

            // -----------------------------------------------------------------
            // 5. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
    }
}
