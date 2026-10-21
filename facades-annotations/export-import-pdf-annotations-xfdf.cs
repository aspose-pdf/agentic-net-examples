using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string targetPdf = "target.pdf";
        const string xfdfPath  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Export annotations from the source PDF to an XFDF file
        // ------------------------------------------------------------
        using (var srcDoc = new Document(sourcePdf))
        using (var xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            // Aspose.Pdf provides ExportAnnotationsToXfdf on Document
            srcDoc.ExportAnnotationsToXfdf(xfdfStream);
        }
        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

        // ------------------------------------------------------------
        // Import the XFDF annotations into the target PDF and save
        // ------------------------------------------------------------
        using (var tgtDoc = new Document(targetPdf))
        {
            // Import the previously exported annotations
            tgtDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting PDF with duplicated annotation layout
            tgtDoc.Save(outputPdf);
        }
        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}
