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
        const string inputPath = "large.pdf";
        const string outputPath = "large_saved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with read/write access – required for incremental saving.
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(fs))
        {
            // ---------- First phase: start async save and interrupt ----------
            using (InterruptMonitor monitor = new InterruptMonitor())
            {
                // Begin saving asynchronously; the operation can be cancelled via the monitor's token.
                Task saveTask = doc.SaveAsync(outputPath, monitor.CancellationToken);

                // Simulate a condition that requires pausing the save (e.g., after 2 seconds).
                await Task.Delay(TimeSpan.FromSeconds(2));

                // Request interruption. This signals the save operation to stop.
                monitor.Interrupt();

                try
                {
                    await saveTask;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Save operation was cancelled.");
                }
            }

            // ---------- Second phase: resume saving ----------
            // Resources used by the first save are now released.
            // Create a new monitor and resume the incremental save.
            using (InterruptMonitor resumeMonitor = new InterruptMonitor())
            {
                // Incremental save continues from where it left off.
                await doc.SaveAsync(resumeMonitor.CancellationToken);
                Console.WriteLine("Save operation resumed and completed.");
            }
        }
    }
}