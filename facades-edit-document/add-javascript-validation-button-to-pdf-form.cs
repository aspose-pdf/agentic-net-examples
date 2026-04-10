using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with form fields
        const string outputPdf = "output.pdf";  // PDF with validation button

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF inside a using block (document disposal rule)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor facade on the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Add a push‑button field that will trigger validation
            // Parameters: field type, name, page number, llx, lly, urx, ury
            formEditor.AddField(FieldType.PushButton, "ValidateBtn", 1, 100, 100, 200, 150);

            // JavaScript that checks all required fields and shows an alert if any are empty
            string js = @"
                var missing = '';
                for (var i = 0; i < this.numFields; i++) {
                    var f = this.getField(this.getFieldName(i));
                    if (f.required && (f.value === '' || f.value === null)) {
                        missing += f.name + '\n';
                    }
                }
                if (missing !== '') {
                    app.alert('Please fill required fields:\n' + missing);
                } else {
                    app.alert('All required fields are filled.');
                }";

            // Attach the JavaScript to the button (replaces any existing script)
            formEditor.SetFieldScript("ValidateBtn", js);

            // Save the modified PDF (save rule)
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with validation button saved to '{outputPdf}'.");
    }
}