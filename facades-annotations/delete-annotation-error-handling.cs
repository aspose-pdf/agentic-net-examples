using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "nonexistent-annotation-id";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                try
                {
                    editor.DeleteAnnotation(annotationName);
                    Console.WriteLine($"Annotation '{annotationName}' deleted.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to delete annotation '{annotationName}': {ex.Message}");
                }

                editor.Save(outputPath);
            }

            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}