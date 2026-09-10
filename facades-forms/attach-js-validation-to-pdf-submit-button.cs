using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing a submit button
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that checks required fields before submitting the form
        string validationScript = @"
var required = ['FirstName', 'LastName', 'Email'];
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (f == null || f.value == '') {
        app.alert('Please fill the required field: ' + required[i]);
        return false; // cancel submission
    }
}
this.submitForm(); // all required fields are filled, submit the form
";

        // Use FormEditor (a SaveableFacade) to attach the script to the submit button
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF
            formEditor.BindPdf(inputPdf);

            // Name of the submit button field (adjust if different in your PDF)
            const string submitButtonName = "btnSubmit";

            // Add the JavaScript to the button; if a script already exists it will be appended
            bool added = formEditor.AddFieldScript(submitButtonName, validationScript);
            if (!added)
            {
                Console.Error.WriteLine($"Failed to add script to button '{submitButtonName}'.");
                return;
            }

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with validation script: {outputPdf}");
    }
}