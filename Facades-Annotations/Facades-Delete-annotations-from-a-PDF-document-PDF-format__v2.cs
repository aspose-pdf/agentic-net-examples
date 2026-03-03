using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF file to the editor
        editor.BindPdf(inputPath);

        // Delete all annotations in the document
        editor.DeleteAnnotations();

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"Annotations deleted. Saved to '{outputPath}'.");
    }
}