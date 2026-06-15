using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: input PDF path and output PDF path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: RemoveAnnotations <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the annotation editor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();

            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Save the modified PDF to the specified output path
            editor.Save(outputPath);

            // Release resources associated with the bound document
            editor.Close();

            Console.WriteLine($"All annotations removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}