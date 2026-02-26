using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF that already contains annotations
        const string xfdfFile  = "annotations.xfdf"; // Temporary XFDF file to hold exported data
        const string outputPdf = "output.pdf";     // PDF after re‑importing annotations

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Export all annotations from the source PDF to an XFDF file
        // ------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdf))
        {
            // ExportAnnotationsToXfdf creates the XFDF file at the specified path
            srcDoc.ExportAnnotationsToXfdf(xfdfFile);
        }

        // ------------------------------------------------------------
        // 2. Load (or create) a PDF document and import the XFDF data
        // ------------------------------------------------------------
        using (Document targetDoc = new Document(inputPdf))
        {
            // ImportAnnotationsFromXfdf reads the XFDF file and adds the annotations
            targetDoc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the final PDF that now contains the imported annotations
            targetDoc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfFile}' and imported into '{outputPdf}'.");
    }
}