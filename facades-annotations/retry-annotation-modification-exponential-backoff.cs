using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the annotation bounds using the fully‑qualified Aspose.Pdf.Rectangle
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Perform the annotation modification with exponential backoff retry
        ExecuteWithRetry(() =>
        {
            // Load the document inside the retry block so the file handle is released on each attempt
            using (Document doc = new Document(inputPath))
            {
                // Create the TextAnnotation for the first page (page index is 1‑based)
                TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title = "Note",
                    Contents = "This is a retry‑protected annotation.",
                    Open = true,
                    Icon = TextIcon.Note
                };

                // Add the annotation to the page and save the result
                doc.Pages[1].Annotations.Add(annotation);
                doc.Save(outputPath);
            }
        });
    }

    // Helper that retries an action with exponential backoff for transient file access errors
    static void ExecuteWithRetry(Action action, int maxAttempts = 5, int baseDelayMs = 200)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                action();
                break; // success
            }
            catch (IOException ex) when (IsTransient(ex))
            {
                attempt++;
                if (attempt >= maxAttempts) throw;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt - 1);
                Console.WriteLine($"Transient I/O error ({ex.Message}). Retrying in {delay} ms (attempt {attempt}/{maxAttempts})...");
                Thread.Sleep(delay);
            }
            catch (UnauthorizedAccessException ex)
            {
                attempt++;
                if (attempt >= maxAttempts) throw;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt - 1);
                Console.WriteLine($"Access error ({ex.Message}). Retrying in {delay} ms (attempt {attempt}/{maxAttempts})...");
                Thread.Sleep(delay);
            }
        }
    }

    // Simple heuristic to decide if an IOException is likely transient (e.g., file lock)
    static bool IsTransient(IOException ex)
    {
        const int ERROR_SHARING_VIOLATION = 0x20;
        const int ERROR_LOCK_VIOLATION = 0x21;
        int hresult = System.Runtime.InteropServices.Marshal.GetHRForException(ex) & 0xFFFF;
        return hresult == ERROR_SHARING_VIOLATION || hresult == ERROR_LOCK_VIOLATION;
    }
}