using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "annotations.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize the annotation editor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdfPath);

            // Create the output stream for the XFDF file
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations to the XFDF stream
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"All annotations exported to '{outputXfdfPath}'.");
    }
}