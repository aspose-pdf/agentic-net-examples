using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable, so we can use a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document into the editor
                editor.BindPdf(inputPath);

                // Remove every annotation from the document – correct method name
                editor.DeleteAnnotations();

                // Save the cleaned PDF to a new file
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
