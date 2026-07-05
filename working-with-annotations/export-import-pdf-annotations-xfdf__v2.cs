using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF (with annotations), target PDF, and temporary XFDF file
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath       = "annotations.xfdf";

        // Ensure source and target files exist
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
            // ---------- Export annotations from source PDF to XFDF ----------
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Export all annotations to an XFDF file
                sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");
            }

            // ---------- Import annotations from XFDF into target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Import annotations from the previously created XFDF file
                targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated target PDF (overwrites the original file)
                targetDoc.Save(targetPdfPath);
                Console.WriteLine($"Annotations imported into target PDF: {targetPdfPath}");
            }

            // Optional: clean up the temporary XFDF file
            if (File.Exists(xfdfPath))
            {
                File.Delete(xfdfPath);
                Console.WriteLine($"Temporary XFDF file deleted: {xfdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}