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
        string inputPdf = "sample.pdf";
        string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input file not found: " + inputPdf);
            return;
        }

        // Create an interrupt monitor and bind it to the current thread
        InterruptMonitor monitor = new InterruptMonitor();
        InterruptMonitor.ThreadLocalInstance = monitor;

        // Run the extraction on a background task
        Task extractionTask = Task.Run(() =>
        {
            try
            {
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(inputPdf);
                    extractor.StartPage = 1;
                    extractor.EndPage = 100; // large range to simulate a long operation

                    // Extraction respects the interrupt monitor internally
                    extractor.ExtractText();

                    // Check cancellation before writing the result
                    if (monitor.CancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation detected before saving text.");
                        return;
                    }

                    extractor.GetText(outputTxt);
                    Console.WriteLine("Extraction completed successfully.");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Extraction was cancelled via interrupt monitor.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error during extraction: " + ex.Message);
            }
        });

        // Simulate a user request to cancel after a short delay
        Thread.Sleep(500);
        monitor.Interrupt(); // Sends cancellation request

        // Wait for the extraction task to finish
        extractionTask.Wait();
    }
}
