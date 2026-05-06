using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitButtonName = "btnSubmit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that checks required fields and submits the form if all are filled
        string js = @"
var required = ['FirstName','LastName','Email'];
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (f && (f.value === '' || f.value === null)) {
        app.alert('Please fill the required field: ' + required[i]);
        f.setFocus();
        return false;
    }
}
this.submitForm({cURL:'https://example.com/submit', cSubmitAs:'PDF'});
";

        // Use FormEditor (Facades API) to bind the PDF and add the script to the submit button
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);
            bool success = editor.AddFieldScript(submitButtonName, js);
            if (!success)
            {
                Console.Error.WriteLine("Failed to attach JavaScript to the submit button.");
            }
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with validation script: {outputPdf}");
    }
}