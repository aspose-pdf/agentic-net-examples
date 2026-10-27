using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_page3.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Ensure the document has at least three pages
        if (pdfDocument.Pages.Count < 3)
        {
            Console.Error.WriteLine("The PDF does not contain a third page.");
            return;
        }

        // Define the rectangle that bounds the annotation (llx, lly, urx, ury)
        // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a TextAnnotation on page 3 with the desired properties.
        // The constructor requires the target Page object and the rectangle.
        TextAnnotation annotation = new TextAnnotation(pdfDocument.Pages[3], rect)
        {
            Title    = "Note",
            Contents = "Semi‑transparent annotation on page 3.",
            Open     = true,
            Icon     = TextIcon.Note,
            Opacity  = 0.6 // 60% opacity for a semi‑transparent effect
        };

        // Add the annotation to the page's annotation collection.
        pdfDocument.Pages[3].Annotations.Add(annotation);

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
