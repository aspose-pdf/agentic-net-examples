using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare a template PDF that contains at least one annotation.
        // ---------------------------------------------------------------------
        const string templatePath = "template.pdf";
        if (!File.Exists(templatePath))
        {
            using (Document templateDoc = new Document())
            {
                // Add a single page.
                Page tmplPage = templateDoc.Pages.Add();

                // Create a simple text annotation.
                TextAnnotation txtAnno = new TextAnnotation(tmplPage, new Rectangle(100, 700, 200, 720))
                {
                    Title = "Sample",
                    Contents = "This is a template annotation",
                    Color = Color.Yellow,
                    Open = true
                };
                tmplPage.Annotations.Add(txtAnno);

                // Save the template so that the annotation editor can bind to it.
                templateDoc.Save(templatePath);
            }
        }

        // ---------------------------------------------------------------------
        // 2. Prepare target PDFs (blank PDFs) that will receive the annotations.
        // ---------------------------------------------------------------------
        string[] targetPaths = { "target1.pdf", "target2.pdf", "target3.pdf" };
        foreach (string target in targetPaths)
        {
            if (!File.Exists(target))
            {
                using (Document targetDoc = new Document())
                {
                    targetDoc.Pages.Add(); // one empty page
                    targetDoc.Save(target);
                }
            }
        }

        // Output folder for the annotated PDFs.
        const string outputFolder = "AnnotatedOutputs";
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // 3. Export annotations from the template PDF to an in‑memory XFDF stream.
        // ---------------------------------------------------------------------
        using (PdfAnnotationEditor templateEditor = new PdfAnnotationEditor())
        {
            templateEditor.BindPdf(templatePath);

            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations to the XFDF stream.
                templateEditor.ExportAnnotationsToXfdf(xfdfStream);
                // Reset the stream position so it can be read.
                xfdfStream.Position = 0;

                // -----------------------------------------------------------------
                // 4. Import the exported annotations into each target PDF and save.
                // -----------------------------------------------------------------
                foreach (string targetPath in targetPaths)
                {
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(targetPath));

                    using (PdfAnnotationEditor targetEditor = new PdfAnnotationEditor())
                    {
                        targetEditor.BindPdf(targetPath);

                        // Import annotations from the XFDF stream.
                        targetEditor.ImportAnnotationsFromXfdf(xfdfStream);

                        // Save the annotated PDF.
                        targetEditor.Save(outputPath);
                    }

                    // Reset the stream for the next import.
                    xfdfStream.Position = 0;
                }
            }
        }

        Console.WriteLine("Annotations copied to target PDFs successfully.");
    }
}
