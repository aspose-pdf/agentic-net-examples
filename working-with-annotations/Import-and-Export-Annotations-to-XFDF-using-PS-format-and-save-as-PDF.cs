using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // LoadOptions for PS format

class Program
{
    static void Main()
    {
        // Paths for the source PS file, the intermediate XFDF file, and the final PDF output
        const string psInputPath   = "input.ps";
        const string xfdfPath      = "annotations.xfdf";
        const string pdfOutputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        // Load the PostScript file (PS is input‑only). PsLoadOptions is in Aspose.Pdf namespace.
        using (Document doc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Export any annotations present in the document to an XFDF file.
            // (If the source has no annotations, an empty XFDF file will be created.)
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // Import the annotations back from the XFDF file.
            // This demonstrates a round‑trip; in real scenarios you might modify the XFDF first.
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

            // Save the resulting document as a PDF.
            doc.Save(pdfOutputPath);
            Console.WriteLine($"PDF saved to: {pdfOutputPath}");
        }
    }
}