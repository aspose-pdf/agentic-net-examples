using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "validated_output.pdf";
        const string emailFieldName = "email"; // name of the email field in the PDF form

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Find the email field by its full name
            if (!form.HasField(emailFieldName))
            {
                Console.Error.WriteLine($"Form does not contain a field named '{emailFieldName}'.");
                return;
            }

            // The field is a TextBoxField (or derived). Cast to access Actions.
            TextBoxField emailField = (TextBoxField)form[emailFieldName];

            // JavaScript that validates the presence of '@' in the field value.
            // If validation fails, set event.rc = false to prevent the value from being accepted.
            string js = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Invalid email address. Please include an ""@"" character.');
    event.rc = false;
}";
            // Assign the validation action
            emailField.Actions.OnValidate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with email validation to '{outputPath}'.");
    }
}