using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath      = "annotations.xfdf";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (required for the facade to work on the same instance)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor facade and bind it to the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(pdfDoc);

                // -------------------------------------------------
                // Export all annotations to an XFDF file
                // -------------------------------------------------
                using (FileStream exportStream = File.Create(xfdfPath))
                {
                    annotEditor.ExportAnnotationsToXfdf(exportStream);
                }
                Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

                // -------------------------------------------------
                // (Optional) Import the same XFDF back into the PDF
                // -------------------------------------------------
                using (FileStream importStream = File.OpenRead(xfdfPath))
                {
                    annotEditor.ImportAnnotationsFromXfdf(importStream);
                }
                Console.WriteLine("Annotations imported back from XFDF.");

                // -------------------------------------------------
                // Save the modified PDF document
                // -------------------------------------------------
                annotEditor.Save(outputPdfPath);
                Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
            }
        }
    }
}