using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_validation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button that will trigger validation
                // Parameters: field type, name, page number, lower-left x, lower-left y, upper-right x, upper-right y
                formEditor.AddField(FieldType.PushButton, "ValidateBtn", 1, 100, 100, 200, 130);

                // JavaScript code to check required fields and show an alert if any are empty
                string js = @"
var missing = [];
for (var i = 0; i < this.numFields; i++) {
    var f = this.getField(this.getFieldName(i));
    if (f.required && (f.value === null || f.value === '')) {
        missing.push(f.name);
    }
}
if (missing.length > 0) {
    app.alert('Please fill the required fields: ' + missing.join(', '));
} else {
    app.alert('All required fields are filled.');
}
";

                // Attach the JavaScript to the button (executed on mouse up)
                formEditor.SetFieldScript("ValidateBtn", js);

                // Save the modified PDF with the validation button
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with validation button: {outputPath}");
    }
}