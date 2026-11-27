using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Bind the PDF to the editor.
            var editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPdf);

            // Export all annotations to an XFDF file.
            using (var fs = File.Create(outputXfdf))
            {
                editor.ExportAnnotationsToXfdf(fs);
            }

            Console.WriteLine($"Annotations successfully exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during annotation export: {ex.Message}");
        }
    }
}
