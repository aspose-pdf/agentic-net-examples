using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor (creation rule)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the PDF (load rule)
        editor.BindPdf(inputPath);

        const int maxAttempts = 2;
        int attempt = 0;
        bool succeeded = false;

        while (!succeeded && attempt < maxAttempts)
        {
            attempt++;
            try
            {
                // Delete all annotations (operation to retry)
                editor.DeleteAnnotations();

                // Save the modified PDF (save rule)
                editor.Save(outputPath);

                succeeded = true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                if (attempt < maxAttempts)
                {
                    // Rebind the PDF and retry
                    editor.Close(); // release previous binding
                    editor = new PdfAnnotationEditor(); // recreate per creation rule
                    editor.BindPdf(inputPath); // rebind per load rule
                }
                else
                {
                    Console.Error.WriteLine("All retry attempts exhausted.");
                }
            }
        }

        // Ensure the facade is properly closed
        editor.Close();
    }
}