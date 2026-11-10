using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // required per task specification

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "filled.pdf";
        const int timeoutSeconds = 10; // abort if longer than this

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a cancellation token that triggers after the timeout
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
        CancellationToken token = cts.Token;

        try
        {
            // Run the filling operation on a background task so it can be cancelled
            Task fillTask = Task.Run(() => FillForm(inputPath, outputPath, token), token);
            fillTask.Wait(token); // wait respecting the token
            Console.WriteLine($"Form filled and saved to '{outputPath}'.");
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("Filling operation timed out and was cancelled.");
        }
        catch (AggregateException ae)
        {
            foreach (var ex in ae.InnerExceptions)
                Console.Error.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    static void FillForm(string inputFile, string outputFile, CancellationToken token)
    {
        // Load the PDF document
        using (Document doc = new Document(inputFile))
        {
            // Sample data to populate the form
            var fieldValues = new Dictionary<string, string>
            {
                { "Name", "John Doe" },
                { "Date", DateTime.Today.ToShortDateString() },
                { "Total", "123.45" }
            };

            // Iterate over each entry and set the corresponding form field
            foreach (var kvp in fieldValues)
            {
                token.ThrowIfCancellationRequested(); // abort if timeout reached

                // Retrieve the widget annotation from the form collection
                WidgetAnnotation widget = doc.Form[kvp.Key] as WidgetAnnotation;
                if (widget == null)
                    continue; // field not found or not a widget

                // Cast the widget to a Field to access the Value property
                Field field = widget as Field;
                if (field != null)
                    field.Value = kvp.Value;
            }

            token.ThrowIfCancellationRequested(); // final check before saving
            doc.Save(outputFile);
        }
    }
}