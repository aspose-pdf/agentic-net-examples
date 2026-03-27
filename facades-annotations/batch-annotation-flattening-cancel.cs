using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Multithreading;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create an interrupt monitor which provides a cancellation token.
        using (InterruptMonitor interruptMonitor = new InterruptMonitor())
        {
            // Link the monitor's token to a CancellationTokenSource for Task cancellation.
            CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(interruptMonitor.CancellationToken);

            // Run the flattening operation on a background task.
            Task flattenTask = Task.Run(() =>
            {
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);
                    editor.FlatteningAnnotations();
                    editor.Save(outputPath);
                }
            }, linkedCts.Token);

            Console.WriteLine("Press 'C' to cancel the flattening operation...");
            while (!flattenTask.IsCompleted)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.C)
                {
                    // Request interruption – the monitor will signal cancellation.
                    interruptMonitor.Interrupt();
                    break;
                }
                Thread.Sleep(100);
            }

            try
            {
                flattenTask.Wait();
                if (!flattenTask.IsCanceled)
                {
                    Console.WriteLine($"Flattening completed. Output saved to '{outputPath}'.");
                }
            }
            catch (AggregateException aggEx)
            {
                if (aggEx.InnerException is OperationCanceledException)
                {
                    Console.WriteLine("Flattening operation was cancelled by the user.");
                }
                else
                {
                    Console.Error.WriteLine($"Error during flattening: {aggEx.InnerException?.Message}");
                }
            }
        }
    }
}