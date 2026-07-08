using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        try
        {
            ModifyAnnotationsWithRetry(inputPdf, outputPdf);
            Console.WriteLine($"Annotations processed and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Operation failed: {ex.Message}");
        }
    }

    // Performs annotation modifications with exponential back‑off retry for transient I/O errors.
    static void ModifyAnnotationsWithRetry(string inputPath, string outputPath)
    {
        const int maxAttempts = 5;          // maximum number of retries
        int attempt = 0;
        int delayMs = 500;                  // initial back‑off delay (ms)

        while (true)
        {
            try
            {
                // Use the Facade to bind, modify, and save the PDF.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);

                    // Example modification: delete all existing annotations.
                    editor.DeleteAnnotations();

                    // Save the modified document.
                    editor.Save(outputPath);
                }

                // Success – exit the retry loop.
                break;
            }
            catch (IOException ioEx) when (IsTransient(ioEx))
            {
                // Transient file access error – apply exponential back‑off.
                attempt++;
                if (attempt >= maxAttempts)
                    throw; // re‑throw after exceeding retries

                Thread.Sleep(delayMs);
                delayMs *= 2; // exponential increase
            }
        }
    }

    // Determines whether an IOException is likely transient (e.g., sharing violation).
    static bool IsTransient(IOException ex)
    {
        // ERROR_SHARING_VIOLATION (0x20) has HResult -2147024864.
        // Adjust this check as needed for other transient scenarios.
        return ex.HResult == -2147024864;
    }
}