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
        const string xfdfPath       = "annotations.xfdf";
        const string outputPdfPath  = "target_with_annotations.pdf";

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

        // Export annotations from the source PDF to an XFDF file
        using (Document sourceDoc = new Document(sourcePdfPath))
        {
            sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // Import the XFDF annotations into the target PDF and save the result
        using (Document targetDoc = new Document(targetPdfPath))
        {
            targetDoc.ImportAnnotationsFromXfdf(xfdfPath);
            targetDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and imported into '{outputPdfPath}'.");
    }
}