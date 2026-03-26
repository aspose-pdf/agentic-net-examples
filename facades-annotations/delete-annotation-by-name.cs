using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists. If it does not, create a minimal PDF with the target annotation.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdfWithAnnotation(inputPath);
        }

        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            // Delete the annotation named "Comment1"
            editor.DeleteAnnotation("Comment1");
            editor.Save(outputPath);
        }
    }

    private static void CreateSamplePdfWithAnnotation(string path)
    {
        // Create a new PDF document with a single page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define a rectangle for the annotation (coordinates are in points).
            // Use Aspose.Pdf.Rectangle (annotation bounds) – lower‑left X/Y and upper‑right X/Y.
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 700);

            // Create a text annotation and give it the required name.
            var comment = new TextAnnotation(page, rect)
            {
                Name = "Comment1",
                Title = "Sample Comment",
                Contents = "This is a sample comment annotation."
            };

            // Add the annotation to the page.
            page.Annotations.Add(comment);

            // Save the PDF to the specified path.
            doc.Save(path);
        }
    }
}
