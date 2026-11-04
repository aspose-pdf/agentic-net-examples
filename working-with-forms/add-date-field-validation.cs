using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF containing the date picker field
        const string outputPath = "validated_output.pdf";
        const string fieldName = "DatePicker";         // Exact name of the date picker field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object via the Document
            var pdfForm = doc.Form;

            // Verify the field exists in the form's Fields collection
            var field = pdfForm?.Fields?.FirstOrDefault(f =>
                string.Equals(f.FullName, fieldName, StringComparison.Ordinal) ||
                string.Equals(f.Name, fieldName, StringComparison.Ordinal));

            if (field == null)
            {
                Console.Error.WriteLine($"Form does not contain a field named '{fieldName}'.");
                return;
            }

            // Aspose.Pdf does not expose a dedicated DateTimeField class in all versions.
            // A date picker is typically a TextBoxField with a JavaScript validation action.
            // Cast to TextBoxField (the base type for text‑based form fields).
            TextBoxField dateField = field as TextBoxField;
            if (dateField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField (date picker).);");
                return;
            }

            // JavaScript that validates the entered date is on/after 2000‑01‑01
            string validationJs = @"
                var minDate = new Date('2000-01-01');
                var entered = util.scand('yyyy-mm-dd', event.value);
                if (entered < minDate) {
                    app.alert('Date must be on or after January 1, 2000.');
                    event.rc = false; // Reject the value
                }
            ";

            // Attach the JavaScript to the field's OnValidate action
            dateField.Actions.OnValidate = new JavascriptAction(validationJs);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with validation to '{outputPath}'.");
    }
}
