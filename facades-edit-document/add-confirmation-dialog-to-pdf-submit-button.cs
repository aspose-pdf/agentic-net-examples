using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing a submit button
        const string outputPdf = "output_with_confirm.pdf";
        const string submitButtonName = "SubmitBtn";   // exact name of the push‑button field
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the FormEditor facade
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);

        // Ensure the button performs a submit to the desired URL
        formEditor.SetSubmitUrl(submitButtonName, submitUrl);

        // JavaScript that shows a confirmation dialog.
        // app.alert returns 4 when the user clicks "Yes" in a question dialog (type 3).
        string confirmJs = @"
if (app.alert('Are you sure you want to submit the form?', 3) == 4) {
    this.submitForm();
}";
        // Attach the script to the button (replaces any existing script)
        formEditor.SetFieldScript(submitButtonName, confirmJs);

        // Save the modified PDF
        formEditor.Save(outputPdf);
        formEditor.Close();

        Console.WriteLine($"PDF saved with confirmation script: {outputPdf}");
    }
}