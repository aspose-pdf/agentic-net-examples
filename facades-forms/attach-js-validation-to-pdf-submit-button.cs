using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // JavaScript that checks required fields before submitting the form.
        // Adjust the field names in the array to match the actual form field names.
        string validationJs = @"
var required = ['field1','field2']; // names of required fields
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (!f || f.value === '') {
        app.alert('Please fill the required field: ' + required[i]);
        return;
    }
}
this.submitForm({cURL:'https://example.com/submit', cSubmitAs:'HTML'});
";

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF.
            formEditor.BindPdf(inputPath);

            // Add a push‑button field on page 1.
            // Parameters: field type, field name, page number, llx, lly, urx, ury.
            formEditor.AddField(FieldType.PushButton, "SubmitBtn", 1, 100F, 500F, 200F, 530F);

            // Attach the JavaScript validation code to the button.
            formEditor.AddFieldScript("SubmitBtn", validationJs);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript button saved to '{outputPath}'.");
    }
}
