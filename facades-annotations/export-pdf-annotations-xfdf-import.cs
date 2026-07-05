using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (with annotations), the target PDF,
        // and a temporary XFDF file that will hold the exported annotations.
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string resultPdfPath = "target_with_annotations.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a simple PDF with an annotation if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            using (Document srcDoc = new Document())
            {
                Page page = srcDoc.Pages.Add();
                // Add a sample text annotation.
                var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title    = "Sample",
                    Subject  = "Demo",
                    Contents = "This is a sample annotation."
                };
                page.Annotations.Add(txtAnn);
                srcDoc.Save(sourcePdfPath);
            }
        }

        // ------------------------------------------------------------
        // Ensure the target PDF exists – create a blank PDF with the same page count.
        // ------------------------------------------------------------
        if (!File.Exists(targetPdfPath))
        {
            using (Document tgtDoc = new Document())
            {
                // For demonstration we create a single blank page.
                tgtDoc.Pages.Add();
                tgtDoc.Save(targetPdfPath);
            }
        }

        // ------------------------------------------------------------
        // Export annotations from the source PDF to an XFDF file.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
        {
            exporter.BindPdf(sourcePdfPath);
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                exporter.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        // ------------------------------------------------------------
        // Import the previously exported XFDF into the target PDF.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
        {
            importer.BindPdf(targetPdfPath);
            // ImportAnnotationsFromXfdf has an overload that accepts a file path.
            importer.ImportAnnotationsFromXfdf(xfdfPath);
            importer.Save(resultPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{resultPdfPath}'.");
    }
}
