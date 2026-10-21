using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (use the PDF‑specific Rectangle type).
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a TextAnnotation bound to the first page.
        TextAnnotation textAnno = new TextAnnotation(doc.Pages[1], rect)
        {
            Title    = "Note",
            Contents = "This is an async annotation.",
            Open     = true,
            Icon     = TextIcon.Note
        };

        // Perform the annotation operation on a background thread.
        await Task.Run(() =>
        {
            // PdfAnnotationEditor implements IDisposable in recent versions; use a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the document to the editor.
                editor.BindPdf(doc);
                // Modify (add) the annotation. The range 1‑doc.Pages.Count covers all pages.
                editor.ModifyAnnotations(1, doc.Pages.Count, textAnno);
            }
        });

        // Save the modified document.
        doc.Save(outputPath);

        Console.WriteLine($"Annotation added asynchronously. Output saved to '{outputPath}'.");
    }
}
