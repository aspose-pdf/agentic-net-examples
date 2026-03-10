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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        try
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF preserving all other content
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"Annotations removed. Output saved to '{outputPath}'.");
    }
}