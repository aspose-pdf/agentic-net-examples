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

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Create the annotation editor, bind the PDF, delete all annotations, and save
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        try
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Remove every annotation from the document
            editor.DeleteAnnotations();

            // Save the cleaned PDF to the specified output path
            editor.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"All annotations removed. Output saved to '{outputPath}'.");
    }
}