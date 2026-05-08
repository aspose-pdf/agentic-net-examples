using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (with annotations), the target PDF,
        // the temporary XFDF file, and the final output PDF.
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "target_with_annotations.pdf";

        // Verify that the input files exist.
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Export all annotations from the source PDF to an XFDF file.
        // ------------------------------------------------------------
        using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
        {
            // Bind the source PDF to the editor.
            exporter.BindPdf(sourcePdfPath);

            // Create (or overwrite) the XFDF file and export annotations.
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
            // Bind the target PDF where annotations will be added.
            importer.BindPdf(targetPdfPath);

            // Import all annotations from the XFDF file.
            importer.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the modified PDF to a new file.
            importer.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{outputPdfPath}'.");
    }
}