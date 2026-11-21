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

        string inputPath  = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Use PdfAnnotationEditor to manipulate annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);

                // Delete all annotations from the document
                editor.DeleteAnnotations();

                // If you need to flatten any remaining annotation content, uncomment the line below
                // editor.FlatteningAnnotations(); // correct method name

                // Save the cleaned PDF to the specified output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
