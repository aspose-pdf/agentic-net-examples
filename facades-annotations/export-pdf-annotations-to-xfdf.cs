using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file containing annotations
        const string inputPdfPath = "input.pdf";
        // Output XFDF file that will store the exported annotations
        const string outputXfdfPath = "annotations.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Create a PdfAnnotationEditor facade and bind it to the PDF document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Create the output stream for the XFDF file
            using (FileStream xfdfStream = File.Create(outputXfdfPath))
            {
                // Export all annotations from the bound PDF into the XFDF stream
                editor.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Optionally, you can close the editor explicitly (Dispose will be called by using)
            editor.Close();
        }

        Console.WriteLine($"Annotations exported successfully to '{outputXfdfPath}'.");
    }
}