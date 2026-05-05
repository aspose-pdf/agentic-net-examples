using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Multithreading;

class PdfSaveWithInterrupt
{
    // Demonstrates starting a large PDF save, interrupting it,
    // and then resuming the save operation.
    public static async Task RunAsync(string inputPath, string outputPath)
    {
        // Load the PDF document (ensure the file exists).
        using (Document doc = new Document(inputPath))
        {
            // Create an interrupt monitor which provides a CancellationToken.
            using (InterruptMonitor monitor = new InterruptMonitor())
            {
                // Start the asynchronous save operation.
                // This will write the PDF to the specified file.
                Task saveTask = doc.SaveAsync(outputPath, monitor.CancellationToken);

                // Simulate some processing time before interrupting.
                await Task.Delay(TimeSpan.FromSeconds(2));

                // Request interruption of the ongoing save.
                monitor.Interrupt();

                try
                {
                    // Await the save task; it will throw if cancelled.
                    await saveTask;
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Save operation was interrupted.");
                }

                // After interruption, resume saving.
                // Open the output file for writing (truncate existing content).
                using (FileStream outStream = new FileStream(
                    outputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None))
                {
                    // Start a new async save using the same cancellation token.
                    // This demonstrates resuming the save from the same document.
                    await doc.SaveAsync(outStream, monitor.CancellationToken);
                }

                Console.WriteLine($"Document saved (or resumed) to '{outputPath}'.");
            }
        }
    }

    // Entry point for demonstration.
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfSaveWithInterrupt <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        await RunAsync(inputPath, outputPath);
    }
}