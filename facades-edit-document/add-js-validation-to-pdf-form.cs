using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_with_validation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript that validates required fields and shows an alert if any are empty
        string script = @"
var required = ['FirstName','LastName','Email'];
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (f == null || f.value == '') {
        app.alert('Please fill the required field: ' + required[i]);
        f.setFocus();
        break;
    }
}
";

        // Create a FormEditor for the input PDF and specify the output file
        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        bool added = formEditor.AddFieldScript("Submit", script);
        if (!added)
        {
            Console.Error.WriteLine("Failed to add JavaScript to the Submit button.");
        }
        formEditor.Save();

        Console.WriteLine($"Saved PDF with validation script to '{outputPath}'.");
    }
}
