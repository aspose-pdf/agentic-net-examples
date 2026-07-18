using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "annotations.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF document
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdfPath);

        // Create the output XFDF file stream and export all annotations
        using (FileStream xfdfStream = File.Create(outputXfdfPath))
        {
            editor.ExportAnnotationsToXfdf(xfdfStream);
        }

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"All annotations exported to '{outputXfdfPath}'.");
    }
}