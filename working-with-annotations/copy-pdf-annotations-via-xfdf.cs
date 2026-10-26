using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the destination PDF and the temporary XFDF file
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "merged_annotations.pdf";

        // Verify that the input files exist
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

        // -----------------------------------------------------------------
        // Export annotations from the source PDF to an XFDF file
        // -----------------------------------------------------------------
        using (Document srcDoc = new Document(sourcePdfPath))
        using (FileStream exportStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            // ExportAnnotationsToXfdf writes all annotation data to the provided stream
            srcDoc.ExportAnnotationsToXfdf(exportStream);
        }

        // -----------------------------------------------------------------
        // Import the XFDF file into the target PDF and save the result
        // -----------------------------------------------------------------
        using (Document tgtDoc = new Document(targetPdfPath))
        using (FileStream importStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
        {
            // ImportAnnotationsFromXfdf reads annotation data from the provided stream
            tgtDoc.ImportAnnotationsFromXfdf(importStream);

            // Save the PDF that now contains the copied annotations
            tgtDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations have been copied to '{outputPdfPath}'.");
    }
}
