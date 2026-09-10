using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "filled_output.pdf";
        const int timeoutSeconds = 15; // timeout for the filling operation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a cancellation token source that triggers after the specified timeout
        using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds)))
        {
            try
            {
                // Use the Form facade to bind the PDF and fill form fields
                using (Form form = new Form())
                {
                    form.BindPdf(inputPdf);

                    // Example: fill a text field named "CustomerName"
                    form.FillField("CustomerName", "John Doe");

                    // Retrieve the underlying Document object after filling
                    Document doc = form.Document;

                    // Save the document asynchronously, honoring the cancellation token
                    doc.SaveAsync(outputPdf, cts.Token).GetAwaiter().GetResult();
                }

                Console.WriteLine($"Document successfully saved to '{outputPdf}'.");
            }
            catch (OperationCanceledException)
            {
                Console.Error.WriteLine("Filling operation was cancelled due to timeout.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}