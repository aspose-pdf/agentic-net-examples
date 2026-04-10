using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonName = "btnSubmit";               // name of the submit button in the PDF
        const string submitUrl  = "https://example.com/submit"; // URL to which the form will be posted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript that shows a Yes/No confirmation dialog.
        // app.alert returns 4 for "Yes" when using type 2 (Yes/No).
        // If the user confirms, the form is submitted; otherwise nothing happens.
        string js = "if (app.alert('Do you want to submit the form?', 3) == 4) { this.submitForm(); }";

        // Bind the PDF, set the submit URL, attach the JavaScript, and save.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Ensure the button posts to the desired URL.
            formEditor.SetSubmitUrl(buttonName, submitUrl);

            // Attach (or replace) the JavaScript on the button.
            // SetFieldScript replaces any existing script; AddFieldScript would append.
            formEditor.SetFieldScript(buttonName, js);

            // Persist the changes.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with confirmation JavaScript attached: {outputPath}");
    }
}