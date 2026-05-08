using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfFiller
{
    // Asynchronously fills a PDF form and aborts if the operation exceeds the timeout.
    public static async Task FillPdfAsync(string inputPdfPath, string outputPdfPath, TimeSpan timeout)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a cancellation token that triggers after the specified timeout.
        using (CancellationTokenSource cts = new CancellationTokenSource(timeout))
        {
            try
            {
                // Bind the PDF using the Facades Form class.
                using (Form form = new Form())
                {
                    form.BindPdf(inputPdfPath);

                    // Example of filling fields – replace with actual field names/values.
                    form.FillField("FirstName", "John");
                    form.FillField("LastName",  "Doe");
                    form.FillField("Agree",    true);

                    // Retrieve the underlying Document to use the async save API.
                    Document doc = form.Document;

                    // Save the modified document asynchronously, respecting the cancellation token.
                    await doc.SaveAsync(outputPdfPath, cts.Token);
                }

                Console.WriteLine($"PDF successfully filled and saved to '{outputPdfPath}'.");
            }
            catch (OperationCanceledException)
            {
                Console.Error.WriteLine("The filling operation was canceled due to timeout.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF filling: {ex.Message}");
            }
        }
    }

    // Entry point for demonstration.
    static async Task Main(string[] args)
    {
        // Example usage:
        // args[0] = input PDF path, args[1] = output PDF path, args[2] = timeout in seconds (optional).
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfFiller <inputPdf> <outputPdf> [timeoutSeconds]");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        int timeoutSec    = (args.Length >= 3 && int.TryParse(args[2], out int sec)) ? sec : 30;

        await FillPdfAsync(inputPath, outputPath, TimeSpan.FromSeconds(timeoutSec));
    }
}