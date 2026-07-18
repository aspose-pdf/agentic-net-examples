using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: RemoveAnnotations <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor to bind the PDF, delete all annotations, and save the result
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF document
            editor.DeleteAnnotations();         // Remove every annotation in the document
            editor.Save(outputPath);            // Save the modified PDF to the specified output path
        }

        Console.WriteLine($"All annotations removed. Output saved to '{outputPath}'.");
    }
}