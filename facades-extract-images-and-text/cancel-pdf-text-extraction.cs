using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfExtractionWithCancellation
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Directory where extracted page texts will be saved
        const string outputDir = "ExtractedPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Create a cancellation token source that can be triggered by the user
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // Start a background task that listens for a key press to cancel the operation
            Thread cancelThread = new Thread(() =>
            {
                Console.WriteLine("Press 'c' to cancel extraction...");
                while (true)
                {
                    if (Console.ReadKey(true).KeyChar == 'c')
                    {
                        cts.Cancel();
                        Console.WriteLine("Cancellation requested.");
                        break;
                    }
                }
            });
            cancelThread.IsBackground = true;
            cancelThread.Start();

            // Use PdfExtractor inside a using block to ensure proper disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(inputPdf);

                // Extract all text from the document (required before GetNextPageText)
                extractor.ExtractText();

                // Optional: set page range (here we process the whole document)
                extractor.StartPage = 1;
                extractor.EndPage   = extractor.Document.Pages.Count;

                int currentPage = extractor.StartPage;

                // Loop through each page's text while checking for cancellation
                while (extractor.HasNextPageText())
                {
                    // Abort if cancellation was requested
                    if (cts.Token.IsCancellationRequested)
                    {
                        // Close the extractor to release resources promptly
                        extractor.Close();
                        Console.WriteLine("Extraction aborted by user.");
                        break;
                    }

                    // Build the output file name for the current page
                    string pageTextPath = Path.Combine(outputDir, $"Page_{currentPage}.txt");

                    // Save the current page's text to a file
                    extractor.GetNextPageText(pageTextPath);

                    Console.WriteLine($"Extracted text for page {currentPage} to '{pageTextPath}'");
                    currentPage++;
                }

                // If the loop completed without cancellation, ensure the extractor is closed
                if (!cts.Token.IsCancellationRequested)
                {
                    extractor.Close();
                    Console.WriteLine("Extraction completed successfully.");
                }
            }
        }
    }
}