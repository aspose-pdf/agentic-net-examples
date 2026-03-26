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
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF (read‑only is fine for this demo)
        using (Document sourceDoc = new Document(inputPath))
        {
            // Create an interrupt monitor that provides a cancellation token
            using (InterruptMonitor monitor = new InterruptMonitor())
            {
                // Start asynchronous save operation with the monitor's token
                Task saveTask = sourceDoc.SaveAsync(outputPath, monitor.CancellationToken);

                // Simulate some processing time before requesting interruption
                Thread.Sleep(2000); // 2 seconds – adjust as needed
                monitor.Interrupt();
                Console.WriteLine("Interrupt requested.");

                try
                {
                    // Wait for the save task to finish (it will be cancelled)
                    saveTask.Wait();
                }
                catch (AggregateException aggEx)
                {
                    foreach (Exception inner in aggEx.InnerExceptions)
                    {
                        if (inner is TaskCanceledException)
                        {
                            Console.WriteLine("Save operation was cancelled.");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Error: {inner.Message}");
                        }
                    }
                }

                // Resume saving after the interruption – here we simply save again
                Console.WriteLine("Resuming save operation...");
                // Using a fresh stream ensures the file is overwritten cleanly
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    sourceDoc.Save(outputStream);
                }
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
    }
}