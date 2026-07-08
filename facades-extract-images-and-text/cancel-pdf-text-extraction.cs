using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Multithreading;

class PdfExtractionWithCancellation
{
    // Extracts all text from a PDF and saves it to a .txt file.
    // The operation can be cancelled via the provided CancellationToken.
    public static void ExtractText(string pdfPath, string outputTxtPath, CancellationToken cancellationToken)
    {
        // Validate input file existence.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (uses the standard load rule).
        using (Document doc = new Document(pdfPath))
        {
            // Create the PdfExtractor facade (uses the standard creation rule).
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the loaded document to the extractor.
                extractor.BindPdf(doc);

                // Optional: set page range (extract whole document by default).
                extractor.StartPage = 1;
                extractor.EndPage   = doc.Pages.Count;

                // Create an interrupt monitor that can be signaled from the cancellation token.
                // The monitor implements IInterruptMonitor and is disposable.
                using (InterruptMonitor monitor = new InterruptMonitor())
                {
                    // When the external token is cancelled, request interruption of the extractor.
                    cancellationToken.Register(() => monitor.Interrupt());

                    // Assign the monitor to the extractor's internal processing.
                    // PdfExtractor inherits from Facade, which respects the current interrupt monitor.
                    // (The monitor is automatically consulted during long‑running operations.)

                    try
                    {
                        // Perform the extraction. This call will observe the interrupt monitor
                        // and throw an OperationCanceledException if cancellation is requested.
                        extractor.ExtractText();

                        // Save the extracted text to the specified file.
                        // Document.Save is not needed here; GetText writes the result directly.
                        extractor.GetText(outputTxtPath);
                        Console.WriteLine($"Text extracted successfully to '{outputTxtPath}'.");
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Extraction was cancelled by the user.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Extraction failed: {ex.Message}");
                    }
                }
            }
        }
    }

    // Example usage.
    static void Main()
    {
        string inputPdf   = "sample.pdf";
        string outputTxt  = "sample.txt";

        // Create a CancellationTokenSource that can be triggered by the user.
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Start the extraction on a separate thread so the main thread can listen for input.
            Thread extractionThread = new Thread(() =>
                ExtractText(inputPdf, outputTxt, cts.Token));

            extractionThread.Start();

            Console.WriteLine("Press 'c' to cancel extraction...");
            while (extractionThread.IsAlive)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.C)
                {
                    // Signal cancellation.
                    cts.Cancel();
                    break;
                }

                Thread.Sleep(100);
            }

            extractionThread.Join();
        }
    }
}