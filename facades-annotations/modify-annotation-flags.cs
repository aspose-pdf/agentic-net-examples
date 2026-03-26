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

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal document if
        // it does not. This prevents the FileNotFoundException that
        // occurred in the original sample.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (var tempDoc = new Document())
            {
                // Add a single blank page so that the annotation editor
                // has a page to bind to.
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Load the PDF into the annotation editor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Load the document to obtain a Page object (required for TextAnnotation ctor)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed
            Page page = doc.Pages[1];

            // Define the rectangle (position and size) of the annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // ------------------------------------------------------------
            // Create a TextAnnotation with custom (user‑defined) flags.
            // Aspose.Pdf does not expose a dedicated property for custom
            // flags, but the underlying PDF specification allows any bit
            // in the Flags field. We can therefore cast an integer value
            // to AnnotationFlags and combine it with the standard flags.
            // ------------------------------------------------------------
            const int CustomFlagValue = 0x8000; // Example custom flag (bit 15)
            AnnotationFlags customFlags = (AnnotationFlags)CustomFlagValue;

            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                // Combine built‑in flags with the custom flag using bitwise OR
                Flags = AnnotationFlags.Hidden | AnnotationFlags.NoView | customFlags,
                Title = "Custom Flags",
                Contents = "This annotation has hidden, no‑view, and a custom flag.",
                Color = Aspose.Pdf.Color.Red,
                Open = true
            };

            // Apply the modified annotation to pages 1 through 1
            editor.ModifyAnnotations(1, 1, annotation);
        }

        // Save the updated PDF
        editor.Save(outputPath);
    }
}
