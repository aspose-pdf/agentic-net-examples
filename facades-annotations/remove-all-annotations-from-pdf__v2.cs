using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: RemoveAnnotations <input.pdf> <output.pdf>");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable via SaveableFacade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Remove all annotations
                editor.DeleteAnnotations();

                // Save the cleaned PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}