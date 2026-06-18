using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PdfFiller
{
    // Fills a PDF form field and saves the document.
    // The operation will be cancelled if it exceeds the specified timeout.
    public static async Task FillAndSaveAsync(string inputPdfPath, string outputPdfPath, TimeSpan timeout)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // Create a cancellation token that triggers after the timeout.
        using (CancellationTokenSource cts = new CancellationTokenSource(timeout))
        {
            // Open the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdfPath))
            {
                // Use the Form facade to fill a field (example field name "Name").
                // Adjust the field name and value as needed for your PDF.
                Form form = new Form(doc);
                form.FillField("Name", "John Doe");

                // Save the modified document asynchronously, passing the cancellation token.
                // If the operation exceeds the timeout, an OperationCanceledException will be thrown.
                await doc.SaveAsync(outputPdfPath, cts.Token);
            }
        }
    }

    // Example entry point.
    public static async Task Main(string[] args)
    {
        string inputPath = "input.pdf";
        string outputPath = "filled_output.pdf";
        TimeSpan maxDuration = TimeSpan.FromSeconds(30); // adjust as needed

        try
        {
            await FillAndSaveAsync(inputPath, outputPath, maxDuration);
            Console.WriteLine($"PDF filled and saved to '{outputPath}'.");
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("The filling operation was cancelled due to timeout.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}