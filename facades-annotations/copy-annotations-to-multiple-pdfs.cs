using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create the template PDF (if it does not already exist) and add a sample annotation.
        // ---------------------------------------------------------------------
        const string templatePdf = "template.pdf";
        if (!File.Exists(templatePdf))
        {
            using (Document templateDoc = new Document())
            {
                // Add a single page.
                templateDoc.Pages.Add();

                // Create a simple text annotation.
                TextAnnotation txtAnn = new TextAnnotation(templateDoc)
                {
                    Rect = new Rectangle(100, 600, 300, 650), // left, bottom, right, top
                    Title = "Sample",
                    Subject = "Demo",
                    Contents = "This is a copied annotation."
                };
                templateDoc.Pages[1].Annotations.Add(txtAnn);

                // Save the template PDF.
                templateDoc.Save(templatePdf);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Create the target PDFs (if they do not already exist).
        // ---------------------------------------------------------------------
        string[] targetPdfs = new string[]
        {
            "target1.pdf",
            "target2.pdf",
            "target3.pdf"
        };

        foreach (string targetPath in targetPdfs)
        {
            if (!File.Exists(targetPath))
            {
                using (Document targetDoc = new Document())
                {
                    targetDoc.Pages.Add(); // simple one‑page document
                    targetDoc.Save(targetPath);
                }
            }
        }

        // ---------------------------------------------------------------------
        // 3. Directory where the annotated PDFs will be saved.
        // ---------------------------------------------------------------------
        const string outputDirectory = "AnnotatedOutputs";
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // 4. Export annotations from the template PDF to an in‑memory XFDF stream.
        // ---------------------------------------------------------------------
        using (PdfAnnotationEditor templateEditor = new PdfAnnotationEditor())
        {
            templateEditor.BindPdf(templatePdf);

            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations from the template PDF into the XFDF stream.
                templateEditor.ExportAnnotationsToXfdf(xfdfStream);

                // Reset the stream position so it can be read for each target PDF.
                xfdfStream.Position = 0;

                // -----------------------------------------------------------------
                // 5. Iterate over each target PDF, import the exported annotations, and save.
                // -----------------------------------------------------------------
                foreach (string targetPdfPath in targetPdfs)
                {
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(targetPdfPath));

                    using (PdfAnnotationEditor targetEditor = new PdfAnnotationEditor())
                    {
                        targetEditor.BindPdf(targetPdfPath);
                        targetEditor.ImportAnnotationsFromXfdf(xfdfStream);
                        targetEditor.Save(outputPath);
                    }

                    // Reset the stream position again for the next iteration.
                    xfdfStream.Position = 0;
                }
            }
        }

        Console.WriteLine("Annotation copying completed.");
    }
}
