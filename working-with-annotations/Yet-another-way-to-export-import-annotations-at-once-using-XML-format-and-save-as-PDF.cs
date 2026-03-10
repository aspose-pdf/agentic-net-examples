using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF with annotations
        const string xfdfFile = "annotations.xfdf";   // temporary XFDF (XML) file
        const string outputPdf = "output.pdf";        // final PDF after import

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the original PDF and export its annotations to XFDF.
        // ------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdf))
        {
            // Export all annotations to an XFDF file (XFDF is an XML‑based format)
            srcDoc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to '{xfdfFile}'.");
        }

        // ------------------------------------------------------------
        // 2. Load a fresh copy of the PDF and import the XFDF back.
        // ------------------------------------------------------------
        using (Document destDoc = new Document(inputPdf))
        {
            // Import annotations from the previously saved XFDF file
            destDoc.ImportAnnotationsFromXfdf(xfdfFile);
            Console.WriteLine($"Annotations imported from '{xfdfFile}'.");

            // ------------------------------------------------------------
            // 3. Save the resulting document as a PDF.
            // ------------------------------------------------------------
            destDoc.Save(outputPdf);
            Console.WriteLine($"Resulting PDF saved to '{outputPdf}'.");
        }
    }
}