using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";                     // PDF with original annotations
        const string targetPdf = "target.pdf";                     // PDF to receive annotations
        const string outputPdf = "target_with_annotations.pdf";   // Resulting PDF
        const string xfdfFile = "annotations.xfdf";                // Temporary XFDF file

        // ------------------------------------------------------------
        // Create a sample source PDF with at least one annotation
        // ------------------------------------------------------------
        using (Document srcDoc = new Document())
        {
            Page srcPage = srcDoc.Pages.Add();
            // Add a simple text annotation so there is something to export
            var annRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation txtAnn = new TextAnnotation(srcPage, annRect)
            {
                Title = "Sample",
                Contents = "This is a sample annotation",
                Color = Aspose.Pdf.Color.Yellow
            };
            srcPage.Annotations.Add(txtAnn);
            srcDoc.Save(sourcePdf);
        }

        // ------------------------------------------------------------
        // Create a target PDF (empty) that will receive the annotations
        // ------------------------------------------------------------
        using (Document tgtDoc = new Document())
        {
            tgtDoc.Pages.Add();
            tgtDoc.Save(targetPdf);
        }

        // ------------------------------------------------------------
        // Export all annotations from the source PDF to an XFDF file
        // ------------------------------------------------------------
        using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
        {
            exporter.BindPdf(sourcePdf);
            using (FileStream exportStream = new FileStream(xfdfFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                exporter.ExportAnnotationsToXfdf(exportStream);
            }
        }

        // ------------------------------------------------------------
        // Import the XFDF annotations into the target PDF and save the result
        // ------------------------------------------------------------
        using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
        {
            importer.BindPdf(targetPdf);
            using (FileStream importStream = new FileStream(xfdfFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                importer.ImportAnnotationsFromXfdf(importStream);
            }
            importer.Save(outputPdf);
        }

        Console.WriteLine("Annotations duplicated successfully.");
    }
}
