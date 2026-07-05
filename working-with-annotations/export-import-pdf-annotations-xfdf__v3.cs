using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string targetPdf = "target.pdf";
        const string xfdfFile  = "annotations.xfdf";

        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
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
            tgtDoc.Save("target_with_annotations.pdf");
        }

        Console.WriteLine("Annotations exported to XFDF and imported into target PDF successfully.");
    }
}