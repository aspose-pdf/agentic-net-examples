using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Determines whether an IOException is likely transient.
    static bool IsTransient(IOException ex)
    {
        // Simplified check: treat all IOExceptions as transient for this example.
        return true;
    }

    // Executes an action with exponential backoff retry on transient file access errors.
    static void Retry(Action action, int maxAttempts = 5, int baseDelayMs = 500)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                action();
                break; // Success
            }
            catch (IOException ex) when (IsTransient(ex))
            {
                attempt++;
                if (attempt >= maxAttempts) throw;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt - 1);
                Thread.Sleep(delay);
            }
            catch (UnauthorizedAccessException)
            {
                attempt++;
                if (attempt >= maxAttempts) throw;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt - 1);
                Thread.Sleep(delay);
            }
        }
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the annotation rectangle (llx, lly, urx, ury).
        Rectangle annotationRect = new Rectangle(100, 500, 300, 550);

        // Perform the annotation modification with retry policy.
        Retry(() =>
        {
            // Load the source PDF.
            using (Document doc = new Document(inputPath))
            {
                // Get the first page (pages are 1‑based).
                Page page = doc.Pages[1];

                // Create a text annotation using the (Page, Rectangle) constructor.
                TextAnnotation txtAnn = new TextAnnotation(page, annotationRect)
                {
                    Title = "Sample Note",
                    Contents = "This annotation was added with exponential backoff retry."
                };

                // Add the annotation to the page.
                page.Annotations.Add(txtAnn);

                // Save the modified PDF.
                doc.Save(outputPath);
            }
        });

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
