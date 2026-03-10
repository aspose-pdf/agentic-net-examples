using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PCL, temporary XFDF, and final PDF
        const string pclPath      = "input.pcl";
        const string xfdfPath     = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Input file not found: {pclPath}");
            return;
        }

        // Load the PCL file (PCL is input‑only, use PclLoadOptions)
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // -------------------------------------------------
            // Export existing annotations (if any) to XFDF file
            // -------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // -------------------------------------------------
            // (Optional) Modify the XFDF file here if needed
            // -------------------------------------------------
            // For demonstration we simply re‑import the same XFDF.

            // -------------------------------------------------
            // Import annotations from the XFDF back into the document
            // -------------------------------------------------
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

            // -------------------------------------------------
            // Save the resulting document as PDF
            // -------------------------------------------------
            doc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to: {outputPdfPath}");
        }
    }
}