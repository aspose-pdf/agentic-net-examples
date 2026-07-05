using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms; // <-- needed for TextBoxField and other form types

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF with form fields
        const string outputPdf = "output.pdf"; // PDF after adding validation button

        // ------------------------------------------------------------
        // Ensure a source PDF exists. If it does not, create a minimal PDF
        // containing a required text field so the example can run
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a new PDF document
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Add a required text box field named "Name"
            var rect = new Aspose.Pdf.Rectangle(100f, 700f, 250f, 720f); // Rectangle expects float values
            TextBoxField nameField = new TextBoxField(page, rect)
            {
                PartialName = "Name",
                Required = true,
                Value = ""
            };
            doc.Form.Add(nameField);

            // Save the placeholder PDF
            doc.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // Add a push‑button that runs JavaScript validation when clicked
        // ------------------------------------------------------------
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF
            formEditor.BindPdf(inputPdf);

            // Add a push‑button field (coordinates are float values)
            formEditor.AddField(
                FieldType.PushButton,
                "ValidateBtn",
                1,
                100f, // LLX
                700f, // LLY
                200f, // URX
                750f  // URY
            );

            // JavaScript that checks every required field.
            // If any required field is empty, an alert is shown and the script stops.
            // Otherwise a success message is displayed.
            string validationJs = @"
                for (var i = 0; i < this.numFields; i++) {
                    var f = this.getField(this.getFieldName(i));
                    if (f.required && (f.value == null || f.value == '')) {
                        app.alert('Please fill required field: ' + f.name);
                        return;
                    }
                }
                app.alert('All required fields are filled.');
            ";

            // Attach the script to the button's mouse‑up action
            // The overload without a script type defaults to JavaScript.
            formEditor.SetFieldScript("ValidateBtn", validationJs);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Validation button added and saved to '{outputPdf}'.");
    }
}
