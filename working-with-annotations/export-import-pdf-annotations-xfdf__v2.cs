using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF (with annotations), destination PDF, and temporary XFDF file
        const string sourcePdfPath = "source_with_annotations.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath       = "annotations.xfdf";

        // Verify source files exist
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
            // ---------- Export annotations from the source PDF ----------
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Export all annotations to an XFDF file
                sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");
            }

            // ---------- Import annotations into the target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Import annotations from the previously created XFDF file
                targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF (you may choose a different output name)
                const string outputPdfPath = "target_with_imported_annotations.pdf";
                targetDoc.Save(outputPdfPath);
                Console.WriteLine($"Annotations imported and saved to: {outputPdfPath}");
            }

            // Optional: clean up the temporary XFDF file
            if (File.Exists(xfdfPath))
            {
                File.Delete(xfdfPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}