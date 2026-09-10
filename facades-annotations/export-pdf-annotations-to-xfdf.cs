using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportAnnotationsExample
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Path to the destination XFDF file
        const string xfdfPath = "annotations.xfdf";

        // Ensure the source PDF exists
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

            // Create (or overwrite) the XFDF output file as a stream
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                // Export all annotations from the bound PDF into the XFDF stream
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Optionally, you can close the editor explicitly (Dispose will be called by using)
            editor.Close();
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}