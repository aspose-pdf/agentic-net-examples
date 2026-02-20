using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, temporary XFDF file, and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        // Create a PdfAnnotationEditor facade to work with annotations
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            annotationEditor.BindPdf(inputPdfPath);

            // Export all annotations to an XFDF file
            using (FileStream xfdfExportStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                annotationEditor.ExportAnnotationsToXfdf(xfdfExportStream);
            }

            // (Optional) modify the XFDF file here if needed

            // Import the annotations back from the XFDF file
            using (FileStream xfdfImportStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                annotationEditor.ImportAnnotationsFromXfdf(xfdfImportStream);
            }

            // Save the resulting PDF document
            annotationEditor.Save(outputPdfPath);
        }
    }
}