using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitButtonName = "btnSubmit"; // change to the actual button name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and edit its form using FormEditor (facade API)
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPath);

            // JavaScript that checks all required fields before allowing submission
            string validationJs = @"
var f = this;
for (var i = 0; i < f.numFields; i++) {
    var field = f.getField(f.getFieldName(i));
    if (field.required && (field.value == null || field.value == '')) {
        app.alert('Please fill the required field: ' + field.name);
        // Cancel the submit action
        event.rc = false;
        break;
    }
}
// If no required fields are empty, the submit proceeds automatically
";

            // Attach the script to the submit button
            bool scriptAdded = editor.AddFieldScript(submitButtonName, validationJs);
            if (!scriptAdded)
            {
                Console.Error.WriteLine($"Failed to add JavaScript to button '{submitButtonName}'.");
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with validation script saved to '{outputPath}'.");
    }
}