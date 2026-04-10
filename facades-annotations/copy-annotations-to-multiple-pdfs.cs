using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains the source annotations
        const string templatePdf = "template.pdf";

        // Ensure the template PDF exists – create a simple one with a sample annotation if missing
        EnsurePdf(templatePdf, doc =>
        {
            // Add a simple text annotation on the first page
            var annotation = new TextAnnotation(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 600, 300, 650)
            )
            {
                Title = "Sample",
                Contents = "This is a template annotation",
                // Use Aspose.Pdf.Color to avoid ambiguity with System.Drawing.Color
                Color = Aspose.Pdf.Color.Yellow
            };
            doc.Pages[1].Annotations.Add(annotation);
        });

        // List of target PDFs that will receive the annotations
        string[] targetPdfs = { "target1.pdf", "target2.pdf", "target3.pdf" };

        // Directory where the annotated PDFs will be saved
        const string outputDir = "Output";
        Directory.CreateDirectory(outputDir);

        foreach (string targetPath in targetPdfs)
        {
            // Ensure the target PDF exists – create an empty one if it does not
            EnsurePdf(targetPath);

            // Build output file name
            string fileName = Path.GetFileNameWithoutExtension(targetPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_annotated.pdf");

            // Use PdfAnnotationEditor to bind the target PDF, import annotations from the template,
            // and save the result. The editor implements IDisposable, so wrap it in a using block.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(targetPath);                         // Load target PDF
                editor.ImportAnnotations(new[] { templatePdf });   // Copy all annotations from template
                editor.Save(outputPath);                            // Save the modified PDF
                // No need to call Close(); the using statement disposes the editor.
            }

            Console.WriteLine($"Annotations copied to '{outputPath}'.");
        }
    }

    /// <summary>
    /// Ensures that a PDF file exists at the specified path. If the file does not exist,
    /// a new PDF is created, optionally allowing the caller to customise its content.
    /// </summary>
    private static void EnsurePdf(string path, Action<Document> customise = null)
    {
        if (File.Exists(path))
            return;

        using (var doc = new Document())
        {
            doc.Pages.Add();
            customise?.Invoke(doc);
            doc.Save(path);
        }
    }
}
