using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath   = "input.pcl";      // PCL source file (input‑only format)
        const string xfdfPath  = "annotations.xfdf"; // Temporary XFDF file for annotations
        const string pdfPath   = "output.pdf";      // Desired PDF output

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file using the dedicated load options (PCL cannot be saved directly)
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Export any existing annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // Re‑import the same XFDF file back into the document (demonstrates round‑trip)
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting document as a PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'. Annotations exported to '{xfdfPath}'.");
    }
}