using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // The annotation name (ID) to be removed – could be obtained at runtime
        string annotationName = "my-annotation-id";

        // ------------------------------------------------------------
        // Ensure the source PDF exists and contains an annotation with
        // the required name. If the file is missing we create a minimal
        // PDF on‑the‑fly so the example can run without external files.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            // Create a new PDF document with a single page
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Add a text annotation and give it the same Name (Id) we will
            // later pass to DeleteAnnotation.
            TextAnnotation txtAnn = new TextAnnotation(page,
                new Aspose.Pdf.Rectangle(100, 600, 200, 650))
            {
                Title    = "Sample",
                Contents = "This annotation will be removed",
                // The Name property is the identifier used by DeleteAnnotation
                Name = annotationName
            };
            page.Annotations.Add(txtAnn);

            // Save the temporary source PDF
            doc.Save(inputPath);
        }

        // ------------------------------------------------------------
        // Delete the annotation whose name matches the variable value
        // ------------------------------------------------------------
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteAnnotation(annotationName);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation '{annotationName}' removed. Result saved to '{outputPath}'.");
    }
}
