using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF, destination PDF and the intermediate XFDF file
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath      = "annotations.xfdf";

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
                Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
            }

            // ---------- Import annotations into the target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Import annotations from the XFDF file
                targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF (overwrites the original target file)
                targetDoc.Save(targetPdfPath);
                Console.WriteLine($"Annotations imported into '{targetPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}