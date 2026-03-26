using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string targetPdf = "target.pdf";
        const string xfdfFile = "annotations.xfdf";
        const string outputPdf = "target_with_annots.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdf}");
            return;
        }

        // Export all annotations from the source PDF to an XFDF file
        using (Document srcDoc = new Document(sourcePdf))
        {
            srcDoc.ExportAnnotationsToXfdf(xfdfFile);
        }

        // Import the XFDF annotations into the target PDF and save the result
        using (Document tgtDoc = new Document(targetPdf))
        {
            tgtDoc.ImportAnnotationsFromXfdf(xfdfFile);
            tgtDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfFile}' and imported into '{outputPdf}'.");
    }
}