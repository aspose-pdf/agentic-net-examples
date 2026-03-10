using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string xfdfPath      = "annotations.xfdf";   // temporary XFDF file
        const string outputPdfPath = "output.pdf";         // final PDF

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, export its annotations to XFDF, re‑import them, and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all annotations to an XFDF file (XML based format)
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // (Optional) At this point you could modify the XFDF file manually.
            // For demonstration we simply import the same XFDF back into the document.
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from '{xfdfPath}'.");

            // Save the document as PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Resulting PDF saved to '{outputPdfPath}'.");
        }

        // Clean up the temporary XFDF file if desired
        try
        {
            if (File.Exists(xfdfPath))
                File.Delete(xfdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary XFDF file: {ex.Message}");
        }
    }
}