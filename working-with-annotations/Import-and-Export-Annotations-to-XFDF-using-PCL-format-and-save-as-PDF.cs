using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPcl = "input.pcl";          // PCL input file
        const string xfdfPath = "annotations.xfdf";   // XFDF file for annotations
        const string outputPdf = "output.pdf";        // Resulting PDF file

        if (!File.Exists(inputPcl))
        {
            Console.Error.WriteLine($"Input file not found: {inputPcl}");
            return;
        }

        try
        {
            // Load the PCL file (PCL is input‑only) using PclLoadOptions
            using (Document doc = new Document(inputPcl, new PclLoadOptions()))
            {
                // Export all annotations (if any) to an XFDF file
                doc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

                // Import the same XFDF back into the document (demonstration)
                doc.ImportAnnotationsFromXfdf(xfdfPath);
                Console.WriteLine($"Annotations imported from '{xfdfPath}'.");

                // Save the document as a PDF (default format)
                doc.Save(outputPdf);
                Console.WriteLine($"PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}