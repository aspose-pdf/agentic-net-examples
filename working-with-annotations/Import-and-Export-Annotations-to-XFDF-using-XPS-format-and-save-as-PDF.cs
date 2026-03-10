using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XFDF related methods

class Program
{
    static void Main()
    {
        const string xpsPath   = "input.xps";          // source XPS file
        const string pdfPath   = "converted.pdf";      // intermediate PDF
        const string xfdfPath  = "annotations.xfdf";   // XFDF file for annotations
        const string finalPdf  = "final_output.pdf";   // PDF after re‑importing annotations

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        // Load the XPS document using XpsLoadOptions (conversion to PDF occurs internally)
        XpsLoadOptions loadOptions = new XpsLoadOptions();
        using (Document doc = new Document(xpsPath, loadOptions))
        {
            // Save the converted document as PDF
            doc.Save(pdfPath);

            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // Import the same XFDF back into the document (demonstrates round‑trip)
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the final PDF that now contains the imported annotations
            doc.Save(finalPdf);
        }

        Console.WriteLine("XPS → PDF conversion, annotation export/import, and final save completed.");
    }
}