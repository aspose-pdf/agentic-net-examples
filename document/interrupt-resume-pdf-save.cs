using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Multithreading;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "large_input.pdf";
        const string outputPdfPath = "large_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the output file as a writable stream.
        // The stream is kept open for the whole operation so that
        // resources can be released and reacquired between save phases.
        using (FileStream outputStream = new FileStream(
            outputPdfPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None))
        {
            // Load the source PDF.
            using (Document document = new Document(inputPdfPath))
            {
                // ---------- First save phase (may be cancelled) ----------
                using (InterruptMonitor interrupt = new InterruptMonitor())
                {
                    // Start asynchronous save using the monitor's cancellation token.
                    Task saveTask = document.SaveAsync(outputStream, interrupt.CancellationToken);

                    // Simulate some processing time before interrupting.
                    Thread.Sleep(2000);

                    // Request interruption of the save operation.
                    interrupt.Interrupt();

                    try
                    {
                        // Wait for the task to complete (it will be cancelled).
                        saveTask.Wait();
                    }
                    catch (AggregateException ae)
                    {
                        // Expected when the operation is cancelled.
                        foreach (var ex in ae.InnerExceptions)
                        {
                            if (ex is OperationCanceledException)
                                Console.WriteLine("Save operation was interrupted.");
                            else
                                Console.WriteLine($"Error during save: {ex.Message}");
                        }
                    }
                }

                // ---------- Resume save phase ----------
                // Reset the stream position if needed (optional, depending on the scenario).
                // Here we simply continue writing to the same stream.
                using (InterruptMonitor resumeInterrupt = new InterruptMonitor())
                {
                    // Start a new asynchronous save to finish the document.
                    Task resumeTask = document.SaveAsync(outputStream, resumeInterrupt.CancellationToken);
                    resumeTask.Wait(); // Wait for completion.
                }
            }
        }

        Console.WriteLine($"Document saved to '{outputPdfPath}'.");
    }
}