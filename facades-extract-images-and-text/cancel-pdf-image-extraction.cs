using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "extracted.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a cancellation token source that can be triggered by the user.
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Start the extraction on a background task.
            Task extractionTask = Task.Run(() =>
                ExtractTextWithCancellation(inputPdf, outputTxt, cts.Token), cts.Token);

            Console.WriteLine("Press 'c' to cancel extraction...");
            while (!extractionTask.IsCompleted)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.C)
                {
                    cts.Cancel(); // Signal cancellation.
                    break;
                }
                Thread.Sleep(100);
            }

            try
            {
                extractionTask.Wait(); // Propagate any exceptions.
                Console.WriteLine("Extraction finished.");
            }
            catch (AggregateException ae)
            {
                if (ae.InnerException is OperationCanceledException)
                    Console.WriteLine("Extraction was cancelled by the user.");
                else
                    Console.Error.WriteLine($"Error: {ae.InnerException?.Message}");
            }
        }
    }

    // Performs text extraction page‑by‑page and respects the cancellation token.
    static void ExtractTextWithCancellation(string pdfPath, string outputPath, CancellationToken token)
    {
        // Get total page count via Document (PdfExtractor no longer exposes PageCount).
        int pageCount;
        using (var doc = new Document(pdfPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Ensure the output file is created safely.
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            for (int page = 1; page <= pageCount; page++)
            {
                // Abort if cancellation was requested.
                token.ThrowIfCancellationRequested();

                using (var extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    // Set the page range to the current page (1‑based).
                    extractor.StartPage = page;
                    extractor.EndPage   = page;
                    extractor.ExtractText();

                    // Get the extracted text via a MemoryStream (GetText has no parameter‑less overload).
                    using (var textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0;
                        using (var reader = new StreamReader(textStream))
                        {
                            string pageText = reader.ReadToEnd();
                            writer.WriteLine($"--- Page {page} ---");
                            writer.WriteLine(pageText);
                        }
                    }
                }
            }
        }
    }
}
