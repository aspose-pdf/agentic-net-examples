using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF, destination PDF, XFDF file and the final output PDF
        const string sourcePdfPath   = "source.pdf";
        const string targetPdfPath   = "target.pdf";
        const string xfdfPath        = "annotations.xfdf";
        const string outputPdfPath   = "target_with_annotations.pdf";

        // Verify that source and target PDFs exist
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

        try
        {
            // ---------- Export annotations from the source PDF to XFDF ----------
            using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
            {
                // Bind the source PDF to the editor
                exporter.BindPdf(sourcePdfPath);

                // Export all annotations to an XFDF file using the Stream overload
                using (FileStream xfdfWriteStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    exporter.ExportAnnotationsToXfdf(xfdfWriteStream);
                }
            }

            // ---------- Import the XFDF into the target PDF and save ----------
            using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
            {
                // Bind the target PDF (the one that will receive the annotations)
                importer.BindPdf(targetPdfPath);

                // Import all annotations from the previously created XFDF file using the Stream overload
                using (FileStream xfdfReadStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    importer.ImportAnnotationsFromXfdf(xfdfReadStream);
                }

                // Save the modified PDF to a new file
                importer.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
