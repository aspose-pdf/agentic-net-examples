using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Facades;            // FormEditor facade

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing a submit button
        const string outputPdf = "output_with_js.pdf"; // Resulting PDF
        const string submitBtnName = "SubmitBtn";      // Fully‑qualified name of the submit button

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialise the FormEditor facade with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // JavaScript that shows a confirmation dialog before the form is submitted.
            // The script uses app.alert; if the user clicks "OK" the form proceeds,
            // otherwise the submission is cancelled.
            string jsCode = @"
                var rc = app.alert('Are you sure you want to submit the form?', 2);
                if (rc != 1) {
                    // Cancel the submit action
                    event.rc = false;
                }
            ";

            // Attach the script to the specified submit button.
            // AddFieldScript adds (or appends) JavaScript to a push‑button field.
            formEditor.AddFieldScript(submitBtnName, jsCode);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}