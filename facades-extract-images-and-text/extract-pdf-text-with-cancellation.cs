using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfExtractionWithCancellation
{
    // Extracts text from a PDF file page‑by‑page.
    // The operation can be cancelled via the provided CancellationToken.
    public static void ExtractText(string pdfPath, string outputPath, CancellationToken cancellationToken)
    {
        // Validate input.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"Input PDF not found: {pdfPath}");

        // Ensure the output directory exists.
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Use the PdfExtractor facade inside a using block for deterministic disposal.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file.
            extractor.BindPdf(pdfPath);

            // Configure the page range to cover the whole document.
            extractor.StartPage = 1;
            extractor.EndPage   = extractor.Document.Pages.Count; // Document property is available after binding.

            // Initiate the extraction process.
            extractor.ExtractText();

            // Open a stream for the final output text file.
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Loop while there are more pages and cancellation has not been requested.
                while (extractor.HasNextPageText())
                {
                    // Abort if the caller signaled cancellation.
                    if (cancellationToken.IsCancellationRequested)
                    {
                        // Clean up the extractor and exit the loop.
                        extractor.Close();
                        Console.WriteLine("Extraction cancelled by user.");
                        break;
                    }

                    // Retrieve the next page's text into the output stream.
                    // The overload with (Stream) writes the page text directly.
                    extractor.GetNextPageText(outStream);
                }
            }

            // If the operation completed without cancellation, ensure the extractor is closed.
            if (!cancellationToken.IsCancellationRequested)
                extractor.Close();
        }
    }

    // Example usage.
    static void Main()
    {
        string inputPdf  = "sample.pdf";
        string outputTxt = "sample_extracted.txt";

        // Create a CancellationTokenSource that can be triggered by the user (e.g., after 5 seconds).
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            // For demonstration, cancel after 5 seconds.
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            try
            {
                ExtractText(inputPdf, outputTxt, cts.Token);
                Console.WriteLine($"Extraction finished. Output saved to '{outputTxt}'.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}