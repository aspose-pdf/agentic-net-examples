using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";      // PDF that already contains annotations
        const string targetPdfPath   = "target.pdf";      // PDF to receive the annotations
        const string xfdfTempPath    = "temp_annotations.xfdf";
        const int startPage          = 2;                 // first page to import (1‑based)
        const int endPage            = 4;                 // last page to import (inclusive)

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
            // ------------------------------------------------------------
            // 1. Export annotations from the source PDF for the desired page range
            // ------------------------------------------------------------
            using (PdfAnnotationEditor exporter = new PdfAnnotationEditor())
            {
                exporter.BindPdf(sourcePdfPath);

                // Export all annotation types for pages startPage..endPage into a temporary XFDF file
                using (FileStream xfdfStream = File.Create(xfdfTempPath))
                {
                    // Use the overload that accepts AnnotationType[] if you need to filter types.
                    // Here we export all types, so we pass null for the types array.
                    exporter.ExportAnnotationsXfdf(xfdfStream, startPage, endPage, (AnnotationType[])null);
                }

                exporter.Close();
            }

            // ------------------------------------------------------------
            // 2. Import the exported XFDF into the target PDF
            // ------------------------------------------------------------
            using (PdfAnnotationEditor importer = new PdfAnnotationEditor())
            {
                importer.BindPdf(targetPdfPath);

                // Import all annotations from the temporary XFDF file
                importer.ImportAnnotationsFromXfdf(xfdfTempPath);

                // Save the updated PDF
                importer.Save("target_with_imported_annotations.pdf");
                importer.Close();
            }

            // Clean up temporary XFDF file
            if (File.Exists(xfdfTempPath))
                File.Delete(xfdfTempPath);

            Console.WriteLine("Annotations imported successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}