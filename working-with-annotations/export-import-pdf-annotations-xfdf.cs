using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "target_with_annotations.pdf";

        // Verify input files exist
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

        // -------------------------------------------------
        // Export annotations from the source PDF to XFDF
        // -------------------------------------------------
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            // Export only the annotation data to an XFDF file
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                sourceDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        // -------------------------------------------------
        // Import the XFDF annotations into the target PDF
        // -------------------------------------------------
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Import the previously exported XFDF file
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the updated PDF
            targetDoc.Save(outputPdfPath);
            Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
        }
    }
}
