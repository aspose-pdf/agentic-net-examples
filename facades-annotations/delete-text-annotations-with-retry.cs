using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Executes an action with exponential backoff retry policy.
    // Retries on IOException or UnauthorizedAccessException, which are common transient file access errors.
    static void ExecuteWithRetry(Action action, int maxAttempts = 5, int baseDelayMs = 200)
    {
        int attempt = 0;
        int delay = baseDelayMs;

        while (true)
        {
            try
            {
                action();
                break; // Success
            }
            catch (IOException)
            {
                attempt++;
                if (attempt >= maxAttempts) throw;
                Thread.Sleep(delay);
                delay *= 2; // Exponential backoff
            }
            catch (UnauthorizedAccessException)
            {
                attempt++;
                if (attempt >= maxAttempts) throw;
                Thread.Sleep(delay);
                delay *= 2;
            }
        }
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf Document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfAnnotationEditor facade (lifecycle rule: use using)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Example annotation modification: delete all text annotations
                ExecuteWithRetry(() => editor.DeleteAnnotations("Text"));

                // Save the modified document (lifecycle rule: use Save method)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}