using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF, destination PDF and temporary XFDF file
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "target_with_comments.pdf";

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
            }

            // ---------- Import annotations into the target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Import the previously exported XFDF file
                targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF with copied comments
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations exported from '{sourcePdfPath}' and imported into '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary XFDF file
            if (File.Exists(xfdfPath))
            {
                try { File.Delete(xfdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}