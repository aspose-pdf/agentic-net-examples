using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to work with annotations.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF file to the editor.
            editor.BindPdf(inputPath);

            // Delete all annotations while keeping the original page content layout.
            editor.DeleteAnnotations();

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations removed. Output saved to '{outputPath}'.");
    }
}