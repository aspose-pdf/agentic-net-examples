using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // source PDF with form fields
        const string outputPdf = "output_form_with_validation.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor binds the source PDF and prepares the output file.
        // The constructor (string inputFile, string outputFile) follows the
        // lifecycle rule: the object is disposed via using and the result is saved
        // with the Save() method (no custom presentation code is introduced).
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a push‑button field that will trigger the validation script.
            // Coordinates are in points; adjust as needed.
            // FieldType.PushButton is defined in Aspose.Pdf.Facades.
            bool added = formEditor.AddField(
                FieldType.PushButton,   // type of the field
                "ValidateBtn",          // field name
                1,                      // page number (1‑based)
                100, 500,               // lower‑left X,Y
                200, 550);              // upper‑right X,Y

            if (!added)
            {
                Console.Error.WriteLine("Failed to add the validation button.");
                return;
            }

            // JavaScript that checks every required field.
            // If a required field is empty, an alert is shown and the script stops.
            // Otherwise, a success message is displayed.
            string jsCode = @"
var fieldNames = this.getFieldNames();
for (var i = 0; i < fieldNames.length; i++) {
    var f = this.getField(fieldNames[i]);
    if (f.required && (f.value == null || f.value == '')) {
        app.alert('Please fill required field: ' + f.name);
        return false;
    }
}
app.alert('All required fields are filled.');
return true;";

            // Attach the JavaScript to the button.
            // SetFieldScript replaces any existing script for the button.
            bool scriptSet = formEditor.SetFieldScript("ValidateBtn", jsCode);
            if (!scriptSet)
            {
                Console.Error.WriteLine("Failed to set JavaScript on the button.");
                return;
            }

            // Persist changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"PDF with validation button saved to '{outputPdf}'.");
    }
}