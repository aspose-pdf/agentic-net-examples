using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string svgPath = "output.svg";
        const string outputPdfPath = "output.pdf";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // -------------------------------------------------
                // Export all annotations to an XFDF file
                // -------------------------------------------------
                pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

                // -------------------------------------------------
                // Convert the PDF to SVG format
                // -------------------------------------------------
                pdfDoc.Save(svgPath, new Aspose.Pdf.SvgSaveOptions());
                Console.WriteLine($"PDF converted to SVG: {svgPath}");

                // -------------------------------------------------
                // Import annotations back from the XFDF file
                // -------------------------------------------------
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);
                Console.WriteLine($"Annotations imported from XFDF: {xfdfPath}");

                // -------------------------------------------------
                // Save the final PDF (with annotations restored)
                // -------------------------------------------------
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Final PDF saved: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}