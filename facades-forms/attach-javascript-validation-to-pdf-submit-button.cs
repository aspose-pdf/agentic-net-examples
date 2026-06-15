using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the submit button
        const string outputPdf = "output.pdf";  // PDF with attached JavaScript

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor to edit the form. The constructor takes input and output paths.
        using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor(inputPdf, outputPdf))
        {
            // Name of the submit button field in the PDF.
            const string submitButtonName = "btnSubmit";

            // JavaScript that validates required fields before submitting.
            // Adjust the field names in the array to match your form.
            string validationScript = @"
var required = ['FirstName', 'LastName', 'Email'];
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (!f || f.value === '' || f.value === null) {
        app.alert('Please fill the required field: ' + required[i]);
        // Prevent the submit action.
        event.rc = false;
        break;
    }
}
";

            // Attach the script to the submit button. AddFieldScript appends the script;
            // SetFieldScript would replace any existing script.
            formEditor.AddFieldScript(submitButtonName, validationScript);

            // Optionally set the URL where the form will be submitted.
            formEditor.SetSubmitUrl(submitButtonName, "https://www.example.com/submit");

            // Save the modified PDF.
            formEditor.Save();
        }

        Console.WriteLine($"JavaScript attached and PDF saved to '{outputPdf}'.");
    }
}