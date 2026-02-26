using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF with annotations
        const string xfdfPath       = "annotations.xfdf";   // temporary XFDF file
        const string outputPdfPath = "output.pdf";         // PDF after re‑importing annotations

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Export existing annotations to XFDF
        // ------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Export all annotations of the source document to an XFDF file
            srcDoc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
        }

        // ------------------------------------------------------------
        // 2. Import the XFDF back into a (new) PDF document
        // ------------------------------------------------------------
        using (Document dstDoc = new Document(inputPdfPath)) // start from same PDF or another one
        {
            // Import annotations from the XFDF file into the document
            dstDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting PDF (annotations now restored)
            dstDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF with imported annotations saved to '{outputPdfPath}'.");
        }

        // Optional clean‑up of the temporary XFDF file
        try
        {
            if (File.Exists(xfdfPath))
                File.Delete(xfdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary XFDF file: {ex.Message}");
        }
    }
}