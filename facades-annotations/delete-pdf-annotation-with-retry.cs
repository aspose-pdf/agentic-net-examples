using System;
using Aspose.Pdf.Facades;

class AnnotationDeletionWithRetry
{
    // Deletes a specific annotation (by name) from a PDF.
    // If the first attempt throws an exception, the PDF is rebound and the operation is retried once.
    public static void DeleteAnnotationWithRetry(string inputPdfPath, string outputPdfPath, string annotationName)
    {
        if (!System.IO.File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfAnnotationEditor does NOT implement IDisposable, so we manage its lifetime manually.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Helper local function to bind the PDF to the editor.
        void Bind()
        {
            // BindPdf(string) initializes the facade with the source document.
            editor.BindPdf(inputPdfPath);
        }

        // First bind.
        Bind();

        bool success = false;
        int attempt = 0;
        const int maxAttempts = 2; // original try + one retry

        while (!success && attempt < maxAttempts)
        {
            try
            {
                attempt++;

                // Attempt to delete the annotation.
                // Use DeleteAnnotation(string) for a specific annotation name.
                editor.DeleteAnnotation(annotationName);

                // Save the modified PDF. Save(string) writes a PDF regardless of extension.
                editor.Save(outputPdfPath);

                // If we reach here, the operation succeeded.
                success = true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Attempt {attempt} failed: {ex.Message}");

                if (attempt < maxAttempts)
                {
                    // Rebind the PDF before retrying.
                    // Close the current binding to release resources.
                    editor.Close();

                    // Recreate a fresh editor instance to avoid stale state.
                    editor = new PdfAnnotationEditor();

                    // Bind again to the original input PDF.
                    Bind();
                }
                else
                {
                    // No more retries; rethrow or handle as needed.
                    Console.Error.WriteLine("All attempts to delete the annotation have failed.");
                }
            }
        }

        // Ensure resources are released.
        editor.Close();
    }

    // Example usage.
    static void Main()
    {
        const string inputPath  = "example.pdf";
        const string outputPath = "example_cleaned.pdf";
        const string annotName  = "4cfa69cd-9bff-49e0-9005-e22a77cebf38";

        DeleteAnnotationWithRetry(inputPath, outputPath, annotName);
        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}