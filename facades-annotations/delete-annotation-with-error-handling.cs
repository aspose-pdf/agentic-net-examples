using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotName  = "nonexistent-annotation-id";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable via SaveableFacade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            try
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Attempt to delete the annotation by its name (ID)
                editor.DeleteAnnotation(annotName);
            }
            catch (Aspose.Pdf.PdfException ex)
            {
                // Aspose.Pdf specific exception – annotation not found or other PDF error
                Console.Error.WriteLine($"PdfException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Any other unexpected exception
                Console.Error.WriteLine($"Error: {ex.Message}");
            }

            // Save the (possibly unchanged) document
            try
            {
                editor.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save document: {ex.Message}");
            }
        }
    }
}
