using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF after deletion
        const string annotationName = "4cfa69cd-9bff-49e0-9005-e22a77cebf38"; // example annotation ID

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        const int maxAttempts = 2; // first try + one retry
        int attempt = 0;
        bool succeeded = false;

        while (attempt < maxAttempts && !succeeded)
        {
            // PdfAnnotationEditor implements IDisposable, so wrap it in using
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                try
                {
                    // Bind the PDF document to the facade
                    editor.BindPdf(inputPdf);

                    // Delete the specific annotation (or use DeleteAnnotations() to remove all)
                    editor.DeleteAnnotation(annotationName);
                    // editor.DeleteAnnotations(); // alternative: delete all annotations

                    // Save the modified PDF
                    editor.Save(outputPdf);

                    succeeded = true; // operation succeeded, exit loop
                }
                catch (Exception ex)
                {
                    // Log the error and prepare for a retry
                    Console.Error.WriteLine($"Attempt {attempt + 1} failed: {ex.Message}");
                    attempt++;

                    // If we have exhausted retries, rethrow the exception
                    if (attempt >= maxAttempts)
                    {
                        Console.Error.WriteLine("All retry attempts exhausted.");
                        throw;
                    }

                    // Otherwise, loop will create a fresh editor and retry
                }
            }
        }

        if (succeeded)
        {
            Console.WriteLine($"Annotation deletion completed. Output saved to '{outputPdf}'.");
        }
    }
}