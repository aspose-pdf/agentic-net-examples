using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        // Paths for the source template PDF and the output filled PDF
        const string inputPdfPath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // Timeout in seconds after which the operation should be aborted
        const int timeoutSeconds = 10;

        // Create a CancellationTokenSource that cancels after the specified timeout
        using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds)))
        {
            CancellationToken token = cts.Token;

            try
            {
                // Run the filling process on a background thread and observe the token
                await Task.Run(() =>
                {
                    // Throw if cancellation was already requested
                    token.ThrowIfCancellationRequested();

                    // Use the Form facade to bind, fill, and save the PDF
                    using (Form form = new Form())
                    {
                        // Bind the source PDF file
                        form.BindPdf(inputPdfPath);
                        token.ThrowIfCancellationRequested();

                        // Example: fill a form field named "Name" with a value
                        form.FillField("Name", "John Doe");
                        token.ThrowIfCancellationRequested();

                        // Save the filled PDF to the output path
                        form.Save(outputPdfPath);
                    }
                }, token);

                Console.WriteLine("PDF filled successfully.");
            }
            catch (OperationCanceledException)
            {
                // The operation exceeded the timeout and was cancelled
                Console.Error.WriteLine("Filling operation timed out and was cancelled.");
            }
            catch (Exception ex)
            {
                // Handle any other errors that may occur
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}