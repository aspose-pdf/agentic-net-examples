using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            ModifyAnnotationsWithRetry(inputPdf, outputPdf);
            Console.WriteLine($"Annotations modified and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Operation failed: {ex.Message}");
        }
    }

    // Retries the annotation modification operation with exponential backoff.
    static void ModifyAnnotationsWithRetry(string sourcePath, string destinationPath)
    {
        const int maxRetries = 5;          // maximum number of attempts
        const int baseDelayMs = 200;       // initial delay in milliseconds

        int attempt = 0;

        while (true)
        {
            try
            {
                // Bind the PDF file to the facade.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(sourcePath);

                    // The template annotation must be created with a page and rectangle.
                    // Use the first page of the document and a zero‑size rectangle because the
                    // rectangle is not used by ModifyAnnotations – only the properties (e.g., Title).
                    Page firstPage = editor.Document.Pages[1];
                    Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                    TextAnnotation template = new TextAnnotation(firstPage, dummyRect)
                    {
                        Title = "Reviewed"
                    };

                    // Apply the modification to the whole document.
                    editor.ModifyAnnotations(1, editor.Document.Pages.Count, template);

                    // Save the modified document.
                    editor.Save(destinationPath);
                }

                // Success – exit the retry loop.
                break;
            }
            catch (IOException ex) when (IsTransient(ex))
            {
                // Transient I/O error (e.g., file locked). Retry with backoff.
                attempt++;
                if (attempt > maxRetries) throw;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt - 1);
                Thread.Sleep(delay);
            }
            catch (UnauthorizedAccessException ex) when (IsTransient(ex))
            {
                // Transient access error. Retry with backoff.
                attempt++;
                if (attempt > maxRetries) throw;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt - 1);
                Thread.Sleep(delay);
            }
        }
    }

    // Determines whether an exception is considered transient for retry purposes.
    static bool IsTransient(Exception ex)
    {
        // Simple heuristic: treat I/O and unauthorized access as transient.
        // Extend this method with more sophisticated checks if needed.
        return ex is IOException || ex is UnauthorizedAccessException;
    }
}
