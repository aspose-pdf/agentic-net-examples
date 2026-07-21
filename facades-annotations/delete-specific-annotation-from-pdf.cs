using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable, so wrap it in a using block
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Delete the annotation whose Name property is "Comment1"
            editor.DeleteAnnotation("Comment1");

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation \"Comment1\" deleted. Output saved to '{outputPath}'.");
    }
}