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
        const string inputPath = "large_input.pdf";
        const string intermediatePath = "partial_output.pdf";
        const string finalPath = "final_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document (read‑only)
        using (Document doc = new Document(inputPath))
        {
            // Create a cancellation source and an interrupt monitor
            using (CancellationTokenSource cts = new CancellationTokenSource())
            using (InterruptMonitor monitor = new InterruptMonitor())
            {
                // Link the monitor's token with the cancellation source
                using (CancellationTokenRegistration reg = monitor.CancellationToken.Register(() => cts.Cancel()))
                {
                    // Start the asynchronous save operation
                    Task saveTask = doc.SaveAsync(intermediatePath, cts.Token);

                    // Simulate some work and then request interruption
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    Console.WriteLine("Requesting interruption of the save operation...");
                    monitor.Interrupt(); // This will cancel the token

                    try
                    {
                        await saveTask;
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Save operation was interrupted.");
                    }
                }
            }

            // At this point resources used by the first save are released.
            // To resume, reopen the document (or continue with the same instance if still valid)
            // and perform another save to complete the operation.
            using (Document resumeDoc = new Document(inputPath))
            {
                Console.WriteLine("Resuming save operation to final output...");
                await resumeDoc.SaveAsync(finalPath, CancellationToken.None);
                Console.WriteLine($"Document saved successfully to '{finalPath}'.");
            }
        }
    }
}