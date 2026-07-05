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
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";
        const int timeoutSeconds  = 30;

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Create a cancellation token that triggers after the timeout
        using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));

        try
        {
            // Bind the PDF template using the Form facade
            using Form form = new Form();
            form.BindPdf(templatePath);

            // Example: fill a text field named "Name"
            form.FillField("Name", "John Doe");

            // Retrieve the underlying Document object
            Document doc = form.Document;

            // Save the modified document asynchronously with cancellation support
            await doc.SaveAsync(outputPath, cts.Token);

            Console.WriteLine($"PDF saved to '{outputPath}'.");
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