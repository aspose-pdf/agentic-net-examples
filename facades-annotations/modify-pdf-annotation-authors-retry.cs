using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Configuration for retry policy
    private const int MaxRetryAttempts = 5;          // maximum number of retries
    private const int BaseDelayMilliseconds = 500;   // initial delay for backoff

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor facade
            PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();

            // Bind the loaded document to the editor
            annotationEditor.BindPdf(doc);

            // Example operation: modify the author of all annotations on pages 1‑5
            Action modifyAction = () =>
                // The method does not expose named parameters; use positional arguments
                annotationEditor.ModifyAnnotationsAuthor(
                    1,               // startPage
                    5,               // endPage
                    "Old Author",  // oldAuthor
                    "New Author"   // newAuthor
                );

            // Execute the modification with retry and exponential backoff
            ExecuteWithRetry(modifyAction);

            // Save the modified document using the facade's Save method
            // (PdfAnnotationEditor inherits SaveableFacade)
            annotationEditor.Save(outputPdfPath);

            // Close the facade (releases the bound document)
            annotationEditor.Close();
        }

        Console.WriteLine($"Annotation modifications saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Executes the supplied action with an exponential backoff retry policy.
    /// Retries on transient file‑access related exceptions.
    /// </summary>
    /// <param name="action">The operation to execute.</param>
    private static void ExecuteWithRetry(Action action)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                action();
                // Success – exit the loop
                break;
            }
            catch (IOException) when (IsTransient())
            {
                attempt++;
                if (attempt > MaxRetryAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation failed.");
                    throw;
                }

                int delay = (int)(BaseDelayMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"Transient I/O error encountered (attempt {attempt}/{MaxRetryAttempts}). Retrying in {delay} ms...");
                Thread.Sleep(delay);
            }
            catch (UnauthorizedAccessException)
            {
                attempt++;
                if (attempt > MaxRetryAttempts)
                {
                    Console.Error.WriteLine("Maximum retry attempts reached. Operation failed.");
                    throw;
                }

                int delay = (int)(BaseDelayMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"Access error encountered (attempt {attempt}/{MaxRetryAttempts}). Retrying in {delay} ms...");
                Thread.Sleep(delay);
            }
        }
    }

    /// <summary>
    /// Determines whether the most recent IOException is likely transient.
    /// In this simplified example we treat any IOException as transient.
    /// </summary>
    private static bool IsTransient()
    {
        // Simple heuristic: treat all IOExceptions as transient for this example.
        // In production you might inspect HResult or message for specific cases.
        return true;
    }
}
