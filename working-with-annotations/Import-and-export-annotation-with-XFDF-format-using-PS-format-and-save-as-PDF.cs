using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XFDF related methods

class Program
{
    static void Main()
    {
        const string psInputPath   = "input.ps";      // PS file to load (input‑only format)
        const string xfdfPath      = "annotations.xfdf"; // temporary XFDF file
        const string outputPdfPath = "output.pdf";    // final PDF output

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Load the PS document using PsLoadOptions (PS is input‑only)
        using (Document doc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) Re‑import the same XFDF back into the document
            // This demonstrates the import functionality; replace with a different XFDF if needed
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the document as a PDF (default format)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and PDF saved to '{outputPdfPath}'.");
    }
}