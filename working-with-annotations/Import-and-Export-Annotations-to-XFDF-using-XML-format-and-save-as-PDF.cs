using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XFDF related classes if needed

class Program
{
    static void Main()
    {
        // Paths for the source PDF, intermediate XFDF file, and the final PDF
        const string inputPdfPath  = "input.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all existing annotations to an XFDF file
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) At this point the XFDF file can be edited manually or programmatically.
            // For demonstration we simply import the same XFDF back into the document.

            // Import annotations from the XFDF file into the PDF document
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the PDF with the (re)imported annotations
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}' and re-imported. Result saved as '{outputPdfPath}'.");
    }
}