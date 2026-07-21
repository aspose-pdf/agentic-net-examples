using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfAnnotationEditor
using Aspose.Pdf;                 // AnnotationType (if needed)

class DeleteAndExportAnnotations
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // PDF after removing the specified annotation type
        const string outputPdfPath = "output_cleaned.pdf";

        // XFDF file that will contain the remaining annotations
        const string outputXfdfPath = "remaining_annotations.xfdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Use PdfAnnotationEditor to manipulate annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdfPath);

            // Delete all annotations of a specific type, e.g., "Highlight"
            // Available type names are the same as those used in Adobe Acrobat
            editor.DeleteAnnotations("Highlight");

            // Save the PDF after deletion (optional, but demonstrates the change)
            editor.Save(outputPdfPath);

            // Export the remaining annotations to an XFDF file
            using (FileStream xfdfStream = File.Create(outputXfdfPath))
            {
                // Export all annotations that are still present in the document
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Annotations of type 'Highlight' removed.");
        Console.WriteLine($"Cleaned PDF saved to: {outputPdfPath}");
        Console.WriteLine($"Remaining annotations exported to XFDF: {outputXfdfPath}");
    }
}