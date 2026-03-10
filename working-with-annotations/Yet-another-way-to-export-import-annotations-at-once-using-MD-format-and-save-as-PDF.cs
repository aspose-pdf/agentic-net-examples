using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF with annotations
        const string xfdfPath      = "annotations.xfdf";   // temporary XFDF file
        const string outputPdfPath = "output.pdf";         // final PDF after import

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, export its annotations to XFDF, then re‑import them and save.
        using (Document doc = new Document(inputPdfPath))
        {
            // Export all annotations to an XFDF file.
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // (Optional) modify the XFDF file here if needed.

            // Import the annotations back into the document.
            doc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine("Annotations imported back into the document.");

            // Save the document as PDF.
            doc.Save(outputPdfPath);
            Console.WriteLine($"Document saved as PDF to '{outputPdfPath}'.");
        }

        // Clean up the temporary XFDF file if it is no longer needed.
        try
        {
            if (File.Exists(xfdfPath))
                File.Delete(xfdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary XFDF file: {ex.Message}");
        }
    }
}