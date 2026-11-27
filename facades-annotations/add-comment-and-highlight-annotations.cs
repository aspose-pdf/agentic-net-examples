using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the PDF document using the high‑level Document API.
        Document doc = new Document(sourcePath);

        // Define rectangles for the annotations (llx, lly, urx, ury).
        // Adjust the coordinates to match your PDF layout.
        Rectangle commentRect   = new Rectangle(100, 500, 300, 550);
        Rectangle highlightRect = new Rectangle(100, 400, 300, 450);

        // ----- Comment (Text) Annotation -----
        // In Aspose.PDF the comment/sticky‑note annotation is called TextAnnotation.
        TextAnnotation comment = new TextAnnotation(doc.Pages[1], commentRect)
        {
            Contents = "This is a comment added via Aspose.Pdf.",
            Title    = "Comment"
        };
        doc.Pages[1].Annotations.Add(comment);

        // ----- Highlight Annotation -----
        HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], highlightRect)
        {
            Color = Color.Yellow
        };
        doc.Pages[1].Annotations.Add(highlight);

        // Save the annotated PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
