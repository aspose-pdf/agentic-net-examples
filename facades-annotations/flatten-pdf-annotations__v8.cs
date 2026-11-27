using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor implements IDisposable, so use a using block
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(inputPath);

                // Flatten all annotations into the page content
                // NOTE: The correct method name is FlatteningAnnotations()
                editor.FlatteningAnnotations();

                // Save the flattened PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
