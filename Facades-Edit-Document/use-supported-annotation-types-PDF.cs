using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // ---------- Text Annotation ----------
            // Define the rectangle for the annotation using the fully qualified type.
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
            TextAnnotation textAnn = new TextAnnotation(page, textRect)
            {
                Title    = "Note",
                Contents = "This is a text annotation.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(textAnn);

            // ---------- Link Annotation (Web URL) ----------
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            LinkAnnotation linkAnn = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue
            };
            // Use GoToURIAction for external URLs (Hyperlink property is not a string).
            linkAnn.Action = new GoToURIAction("https://www.example.com");
            page.Annotations.Add(linkAnn);

            // ---------- Square Annotation (highlight area) ----------
            Aspose.Pdf.Rectangle squareRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            SquareAnnotation squareAnn = new SquareAnnotation(page, squareRect)
            {
                Color    = Aspose.Pdf.Color.LightGray,
                Contents = "Highlighted region"
            };
            page.Annotations.Add(squareAnn);

            // Use the PdfAnnotationEditor facade to bind the modified document and save it.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}