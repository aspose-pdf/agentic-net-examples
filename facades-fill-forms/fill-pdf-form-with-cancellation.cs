using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const int timeoutSeconds = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a cancellation token that triggers after the timeout.
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        try
        {
            using (Document document = new Document(inputPath))
            {
                // NOTE: The InterruptMonitor API is not available in the current Aspose.Pdf version.
                // Cancellation is handled manually inside the processing loop.

                // Simulate a long‑running fill operation.
                foreach (var field in document.Form.Fields)
                {
                    // Check for cancellation request.
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Operation cancelled due to timeout.");
                        throw new OperationCanceledException();
                    }

                    // Only process text box fields; other field types are ignored in this example.
                    if (field is TextBoxField textBox)
                    {
                        // Fill the field with sample data.
                        textBox.Value = "Sample text";
                    }

                    // Simulate work.
                    Thread.Sleep(500);
                }

                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Filling process was aborted.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
