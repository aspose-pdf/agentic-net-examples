using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_validation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the FormEditor facade
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Add a push‑button field that will trigger the validation script
            // Parameters: field type, name, page number, llx, lly, urx, ury
            editor.AddField(FieldType.PushButton, "ValidateBtn", 1, 50, 750, 150, 800);

            // JavaScript that checks required fields and shows an alert if any are empty
            // Adjust the field names in the array to match the actual form fields
            string js = @"
var required = ['FirstName','LastName','Email'];
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (f && (f.value === null || f.value === '')) {
        app.alert('Please fill the required field: ' + required[i]);
        f.setFocus();
        break;
    }
}
";

            // Attach the script to the button (replaces any existing script)
            editor.SetFieldScript("ValidateBtn", js);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with validation button: {outputPath}");
    }
}