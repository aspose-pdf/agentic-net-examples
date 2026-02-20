using System;
using System.IO;
using Aspose.Pdf.Facades;

class DeleteAnnotationsByType
{
    static void Main(string[] args)
    {
        // Expect three arguments: input PDF path, annotation type name, output PDF path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: DeleteAnnotationsByType <input.pdf> <AnnotationType> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string annotationType = args[1];
        string outputPath = args[2];

        // Validate input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Initialize the annotation editor and bind the PDF
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);

                // Delete all annotations of the specified type
                // The method expects the annotation type as a string (e.g., "Highlight", "Link", "Text")
                editor.DeleteAnnotations(annotationType);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"All \"{annotationType}\" annotations have been removed.");
            Console.WriteLine($"Modified PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}