using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // JavaScript that validates required fields and shows an alert if any are empty
        string js = @"
function validateForm() {
    var required = ['Field1', 'Field2', 'Field3']; // replace with actual field names
    for (var i = 0; i < required.length; i++) {
        var f = this.getField(required[i]);
        if (f && (f.value === null || f.value === '')) {
            app.alert('Please fill the required field: ' + required[i]);
            return false;
        }
    }
    return true;
}
validateForm();
";

        // Attach the script to the document open action (executed when the PDF is opened)
        doc.OpenAction = new JavascriptAction(js);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with validation saved to '{outputPath}'.");
    }
}
