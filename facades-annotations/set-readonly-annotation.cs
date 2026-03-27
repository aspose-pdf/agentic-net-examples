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
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            // Create a new PDF with a single blank page.
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Load the PDF so we can obtain a Page object – required for the TextAnnotation ctor
        Document doc = new Document(inputPath);
        Page page = doc.Pages[1];

        // Define the annotation rectangle (left, bottom, right, top). Adjust as needed.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

        // Create the TextAnnotation, set visual properties and apply the ReadOnly flag.
        TextAnnotation annotation = new TextAnnotation(page, rect)
        {
            Title = "Note",
            Contents = "Read‑only annotation",
            Color = Color.Yellow,
            Flags = AnnotationFlags.ReadOnly // Apply the read‑only flag before modification
        };

        // Bind the PDF to the annotation editor.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Apply the annotation to page 1.
        editor.ModifyAnnotations(1, 1, annotation);

        // Save the updated PDF.
        editor.Save(outputPath);
    }
}
