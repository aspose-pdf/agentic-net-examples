using System;
using System.IO;
using Aspose.Pdf.Facades;

class DeleteAnnotationByName
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, annotation name to delete, output PDF path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: DeleteAnnotationByName <input.pdf> <annotationName> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string annotationName = args[1];
        string outputPath = args[2];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Delete the annotation with the specified name
                editor.DeleteAnnotation(annotationName);

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotation \"{annotationName}\" deleted successfully. Output saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}