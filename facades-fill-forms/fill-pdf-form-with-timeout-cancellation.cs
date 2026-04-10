using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled.pdf";
        const int timeoutMilliseconds = 5000; // timeout for the filling operation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Use the Form facade to fill form fields
            using (Form form = new Form())
            {
                form.BindPdf(doc);

                // Example field filling – replace with actual field names/values
                form.FillField("Name", "John Doe");
                form.FillField("Date", DateTime.Now.ToString("yyyy-MM-dd"));
            }

            // Set up a cancellation token that triggers after the specified timeout
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                cts.CancelAfter(timeoutMilliseconds);
                try
                {
                    // Asynchronously save the document with the cancellation token
                    await doc.SaveAsync(outputPdf, cts.Token);
                    Console.WriteLine($"Document saved successfully to '{outputPdf}'.");
                }
                catch (OperationCanceledException)
                {
                    Console.Error.WriteLine("Filling process was cancelled due to timeout.");
                }
            }
        }
    }
}