using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitBtnName = "SubmitBtn"; // name of the submit button in the PDF
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor facade to modify the form
        using (FormEditor editor = new FormEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set the URL where the form will be submitted
            editor.SetSubmitUrl(submitBtnName, submitUrl);

            // Set the submit flag to submit the whole PDF (optional, adjust as needed)
            editor.SetSubmitFlag(submitBtnName, SubmitFormFlag.Pdf);

            // JavaScript that shows a confirmation dialog before posting
            // app.alert returns 4 for "Yes" when using a 3-button dialog (Yes/No/Cancel)
            string js = "if (app.alert('Are you sure you want to submit?', 3) == 4) { this.submitForm(); }";

            // Attach the script to the submit button
            editor.AddFieldScript(submitBtnName, js);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPath}");
    }
}