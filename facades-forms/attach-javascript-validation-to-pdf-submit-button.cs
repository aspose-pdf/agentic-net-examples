using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string buttonName = "btnSubmit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // JavaScript that checks two required fields before submitting.
        // Adjust field names and the submit URL as needed.
        string js = @"
var f1 = this.getField('FirstName');
var f2 = this.getField('LastName');
if (f1.value == '' || f2.value == '') {
    app.alert('Please fill all required fields.');
} else {
    this.submitForm({cURL:'https://www.example.com/submit'});
}
";

        // Initialize FormEditor with the source PDF and the target PDF.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Attach the JavaScript to the push‑button field.
        bool ok = formEditor.SetFieldScript(buttonName, js);
        if (!ok)
        {
            Console.Error.WriteLine("Failed to set JavaScript on the button.");
        }

        // Persist the changes.
        formEditor.Save();

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}