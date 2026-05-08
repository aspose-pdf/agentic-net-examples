using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // PDF containing a text field named "Email"
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that validates the email format on the blur (focus loss) event.
        // It shows an alert if the entered value does not match a simple email pattern.
        string jsCode = @"
if (event.value.match(/^\\S+@\\S+\\.\\S+$/) == null) {
    app.alert('Invalid email address');
}";

        // FormEditor is a Facade class used to edit form fields.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdf);

            // Retrieve the field named "Email". The Document property gives access to the core PDF object.
            // The Form collection is indexed by the full field name.
            // The collection returns a WidgetAnnotation, which can be cast to Field.
            Field emailField = formEditor.Document.Form["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("Field 'Email' not found in the PDF.");
                return;
            }

            // Assign the JavaScript to the OnLostFocus (blur) action of the field.
            emailField.Actions.OnLostFocus = new JavascriptAction(jsCode);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript validation: {outputPdf}");
    }
}
