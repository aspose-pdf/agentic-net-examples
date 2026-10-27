using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction lives here

class Program
{
    static void Main()
    {
        const string inputPdf = "form_with_datepicker.pdf";
        const string outputPdf = "form_with_datepicker_validated.pdf";
        const string fieldName = "DatePicker"; // name of the date picker field in the PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Locate the form field by name
            var field = doc.Form[fieldName];
            if (field == null)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found in the document.");
                return;
            }

            // JavaScript validation script that rejects dates earlier than 2000-01-01.
            string jsValidate = @"
                // Convert the entered value to a Date object.
                var entered = util.scand('yyyy-mm-dd', event.value);
                var minDate = new Date(2000, 0, 1); // January is month 0
                if (entered < minDate) {
                    app.alert('Date must be on or after January 1, 2000.');
                    event.rc = false; // reject the value
                }";

            // Assign the JavaScript to the OnValidate action.
            field.Actions.OnValidate = new JavascriptAction(jsValidate);

            // Set a default value that complies with the rule.
            // Use TextBoxField (or any field that supports a string value) because DateTimeField may not be available in all SDK versions.
            if (field is TextBoxField txtField)
            {
                txtField.Value = "2000-01-01";
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Validated PDF saved to \"{outputPdf}\".");
    }
}
