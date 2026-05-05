using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_without_text.pdf";
        const string xfdfPath = "remaining_annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the annotation editor and bind the source PDF
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPdf);

        // Delete all annotations of the specified type (e.g., Text annotations)
        editor.DeleteAnnotations("Text");

        // Save the PDF after deletion (optional, but shows the result)
        editor.Save(outputPdf);

        // Export the remaining annotations to an XFDF file
        using (FileStream xfdfStream = File.Create(xfdfPath))
        {
            editor.ExportAnnotationsToXfdf(xfdfStream);
        }

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"Deleted Text annotations, saved PDF to '{outputPdf}', and exported remaining annotations to '{xfdfPath}'.");
    }
}