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
        const string inputPath  = "large_input.pdf";
        const string outputPath = "partial_output.pdf";
        const string finalPath  = "final_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the large PDF document (read‑only stream is sufficient for saving)
        using (Document doc = new Document(inputPath))
        {
            // Create an interrupt monitor – it provides a CancellationToken
            using (InterruptMonitor monitor = new InterruptMonitor())
            {
                // Start asynchronous save operation
                Task saveTask = doc.SaveAsync(outputPath, monitor.CancellationToken);

                // Simulate work while the save is in progress
                await Task.Delay(TimeSpan.FromSeconds(2));

                // Request interruption of the ongoing save
                monitor.Interrupt();

                try
                {
                    // Await the task – it will complete with an OperationCanceledException
                    await saveTask;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Save operation was successfully interrupted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error during save: {ex.Message}");
                }
            }

            // At this point resources held by the first save are released.
            // To resume saving, reopen (or reuse) the document and perform a normal save.
            // Here we demonstrate a fresh save without interruption.
            doc.Save(finalPath);
            Console.WriteLine($"Document saved completely to '{finalPath}'.");
        }
    }
}