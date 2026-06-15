using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitBtnName = "btnSubmit";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor facade to edit the form.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF.
            formEditor.BindPdf(inputPdf);

            // Add a submit button if it does not already exist.
            // Parameters: field name, page number (1‑based), label, URL, lower‑left x/y, upper‑right x/y.
            formEditor.AddSubmitBtn(submitBtnName, 1, "Submit", submitUrl, 100, 700, 200, 750);

            // JavaScript that shows a confirmation dialog.
            // app.alert returns 4 for "Yes" when the dialog type is 2 (Yes/No).
            string js = "if(app.alert('Do you want to submit the form?', 2, 0) == 4) { this.submitForm(); }";

            // Attach the script to the button (replaces any existing script).
            formEditor.SetFieldScript(submitBtnName, js);

            // Set the submit flag to HTML format (optional, but ensures proper submission).
            formEditor.SetSubmitFlag(submitBtnName, SubmitFormFlag.Html);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPdf}'.");
    }
}