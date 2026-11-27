using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "draft.pdf";
        const string outputPath = "ModifiedDraft.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor does NOT implement IDisposable; do not use a using block.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the source PDF.
        editor.BindPdf(inputPath);

        // Delete the annotation with the specified name.
        editor.DeleteAnnotation("Comment1");

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}