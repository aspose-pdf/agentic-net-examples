using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int maxAttempts = 2;
        int attempt = 0;
        bool success = false;

        while (attempt < maxAttempts && !success)
        {
            attempt++;
            try
            {
                // Bind the PDF, delete all annotations, and save.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);          // Initialize with the source PDF
                    editor.DeleteAnnotations();         // Remove all annotations
                    editor.Save(outputPath);            // Persist the changes
                }

                success = true;
                Console.WriteLine($"Annotations deleted successfully on attempt {attempt}.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                // If not the last attempt, the loop will retry by rebinding the PDF.
                if (attempt >= maxAttempts)
                {
                    Console.Error.WriteLine("All retry attempts exhausted.");
                }
            }
        }
    }
}