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

        try
        {
            // Create the annotation editor and bind the source PDF
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(inputPath);

            // Remove all annotations from the document
            editor.DeleteAnnotations();

            // Save the resulting PDF to the specified output path
            editor.Save(outputPath);

            // Release resources associated with the editor
            editor.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}