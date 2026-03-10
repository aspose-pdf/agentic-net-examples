using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input MHT, intermediate XFDF, and output PDF
        const string mhtPath   = "input.mht";
        const string xfdfPath  = "annotations.xfdf";
        const string pdfPath   = "output.pdf";

        // Verify the MHT file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        using (Document doc = new Document(mhtPath, new MhtLoadOptions()))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // (Optional) Import the same XFDF back into the document.
            // This demonstrates the round‑trip capability.
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

            // Save the final PDF document
            doc.Save(pdfPath);
            Console.WriteLine($"PDF saved: {pdfPath}");
        }
    }
}