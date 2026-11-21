using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Email"
            // The Form indexer returns a WidgetAnnotation, so cast it to Field first
            Field field = doc.Form["Email"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Form field 'Email' not found or is not a form field.");
                return;
            }

            // Cast to a TextBoxField to access Actions
            TextBoxField emailField = field as TextBoxField;
            if (emailField == null)
            {
                Console.Error.WriteLine("'Email' field is not a text box.");
                return;
            }

            // JavaScript validation: ensure the value contains an '@' character
            string js = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Invalid email address. It must contain an ''@'' character.');
    event.rc = false; // reject the input
}
";

            // Assign the JavaScript action to the field's OnValidate event
            emailField.Actions.OnValidate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript validation to '{outputPath}'.");
    }
}
