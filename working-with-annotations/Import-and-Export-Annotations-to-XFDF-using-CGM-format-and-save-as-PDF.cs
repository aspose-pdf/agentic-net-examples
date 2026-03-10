using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file, temporary XFDF file, and final PDF output
        const string cgmPath = "input.cgm";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify the CGM source exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Load the CGM file (input‑only format) as a PDF document
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to: {xfdfPath}");

            // (Optional) Re‑import the same XFDF back into the document
            // This demonstrates the round‑trip capability
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from: {xfdfPath}");

            // Save the resulting document as a PDF
            doc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to: {outputPdfPath}");
        }
    }
}