using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Multithreading;

class Program
{
    static async Task Main()
    {
        const string inputPdf  = "large_input.pdf";
        const string outputPdf = "large_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        await SaveWithInterruptionAsync(inputPdf, outputPdf);
    }

    // Demonstrates interrupting a long‑running save operation and then resuming it.
    private static async Task SaveWithInterruptionAsync(string inputPath, string outputPath)
    {
        // Load the document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create an interrupt monitor – it provides a CancellationToken that can be
            // passed to the asynchronous save operation.
            using (InterruptMonitor monitor = new InterruptMonitor())
            {
                CancellationToken token = monitor.CancellationToken;

                // Start the asynchronous save. This runs on a background thread and
                // observes the provided cancellation token.
                Task saveTask = doc.SaveAsync(outputPath, token);

                // Simulate some condition that requires the operation to be paused.
                // Here we wait 2 seconds and then request interruption.
                await Task.Delay(TimeSpan.FromSeconds(2));
                monitor.Interrupt(); // Sends a cancellation request.

                try
                {
                    // Await the save task – it will throw OperationCanceledException
                    // when the interrupt request is honored.
                    await saveTask;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Save operation was interrupted to release resources.");
                }

                // At this point resources used by the partially completed save have been
                // released. To resume, simply start a new save operation. If incremental
                // saving is required, the document must have been opened with a writable
                // stream (not shown here). For simplicity we perform a full save again.
                await doc.SaveAsync(outputPath, CancellationToken.None);
                Console.WriteLine("Save operation resumed and completed successfully.");
            }
        }
    }
}