using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // correct namespace for form fields

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const int timeoutSeconds = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Cancellation token that fires after the specified timeout
            using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds)))
            {
                try
                {
                    // Run the filling logic on a background thread so it can be cancelled
                    Task fillTask = Task.Run(() => FillFormFields(doc, cts.Token), cts.Token);
                    fillTask.Wait(cts.Token); // Wait respecting the token

                    doc.Save(outputPath);
                    Console.WriteLine($"Form filled and saved to '{outputPath}'.");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Filling operation was cancelled due to timeout.");
                }
                // Task.Wait can wrap the cancellation in an AggregateException in some .NET versions
                catch (AggregateException ae) when (ae.InnerException is OperationCanceledException)
                {
                    Console.WriteLine("Filling operation was cancelled due to timeout.");
                }
            }
        }
    }

    private static void FillFormFields(Document doc, CancellationToken token)
    {
        if (doc.Form != null && doc.Form.Count > 0)
        {
            foreach (Field field in doc.Form) // Field is from Aspose.Pdf.Forms
            {
                token.ThrowIfCancellationRequested();

                if (field is TextBoxField textBox) // Correct class name for a text box form field
                {
                    textBox.Value = "Sample";
                }
                // Additional field types (e.g., CheckBoxField, RadioButtonField) can be handled here.
            }
        }
    }
}
