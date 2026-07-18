using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";                 // PDF with original annotations
        const string targetPdfPath = "target.pdf";                 // PDF that will receive the annotations
        const string xfdfPath      = "annotations.xfdf";           // Temporary XFDF file
        const string outputPdfPath = "target_with_annotations.pdf"; // Resulting PDF

        // Verify that the input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine("Source or target PDF file not found.");
            return;
        }

        // ------------------------------------------------------------
        // 1. Export annotations from the source PDF to an XFDF file
        // ------------------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // PdfAnnotationEditor works on an existing Document instance
            PdfAnnotationEditor exporter = new PdfAnnotationEditor();
            exporter.BindPdf(sourceDoc);

            // Export all annotations to a file stream (XFDF format)
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                exporter.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Release resources held by the editor
            exporter.Close();
        }

        // ------------------------------------------------------------
        // 2. Import the XFDF annotations into the target PDF
        // ------------------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            PdfAnnotationEditor importer = new PdfAnnotationEditor();
            importer.BindPdf(targetDoc);

            // Import all annotations from the previously created XFDF file
            importer.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the modified document
            targetDoc.Save(outputPdfPath);

            importer.Close();
        }

        Console.WriteLine($"Annotations duplicated successfully to '{outputPdfPath}'.");
    }
}