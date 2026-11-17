using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";          // PDF containing original annotations
        const string targetPdf = "target.pdf";          // PDF to receive the copied annotations
        const string xfdfFile  = "annotations.xfdf";    // Intermediate XFDF file
        const string outputPdf = "target_with_comments.pdf"; // Resulting PDF

        // Verify input files exist
        if (!File.Exists(sourcePdf) || !File.Exists(targetPdf))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        // ---------- Export annotations from the source PDF to XFDF ----------
        using (Document srcDoc = new Document(sourcePdf))
        {
            // Export all annotations to an XFDF file using the core Document API
            using (FileStream xfdfStream = new FileStream(xfdfFile, FileMode.Create, FileAccess.Write))
            {
                srcDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        // ---------- Import the XFDF into the target PDF and save ----------
        using (Document tgtDoc = new Document(targetPdf))
        {
            // Import annotations from the XFDF file using the core Document API
            tgtDoc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the modified document; Save() without options always writes PDF
            tgtDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfFile}' and imported into '{outputPdf}'.");
    }
}
