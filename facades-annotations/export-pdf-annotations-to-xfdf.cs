using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportAnnotationsExample
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output XFDF file path
        const string xfdfPath = "annotations.xfdf";

        // Ensure the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use PdfAnnotationEditor facade to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(pdfPath);

            // Create the output XFDF file stream
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                // Export all annotations to the XFDF stream
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Optionally, save the PDF if any modifications were made (not required for export only)
            // editor.Save("output.pdf");
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}