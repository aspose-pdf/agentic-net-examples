using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PclLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Paths for the source PCL file, intermediate XFDF file and the final PDF output.
        const string pclPath   = "input.pcl";
        const string xfdfPath  = "annotations.xfdf";
        const string pdfPath   = "output.pdf";

        // Verify that the source PCL file exists.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Source file not found: {pclPath}");
            return;
        }

        // Load the PCL document using PclLoadOptions.
        var pclLoadOptions = new PclLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document doc = new Document(pclPath, pclLoadOptions))
        {
            // ------------------------------------------------------------
            // Export all existing annotations (if any) to an XFDF file.
            // ------------------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // ------------------------------------------------------------
            // Import annotations from the XFDF file back into the document.
            // This demonstrates a round‑trip of annotation data.
            // ------------------------------------------------------------
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

            // ------------------------------------------------------------
            // Save the resulting document as a PDF file.
            // No special SaveOptions are required because the target format
            // is PDF (the default for Document.Save(string)).
            // ------------------------------------------------------------
            doc.Save(pdfPath);
            Console.WriteLine($"PDF saved: {pdfPath}");
        }
    }
}