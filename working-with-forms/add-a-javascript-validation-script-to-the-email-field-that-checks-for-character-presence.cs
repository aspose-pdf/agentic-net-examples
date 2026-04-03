using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction
using Aspose.Pdf.Forms;        // Access form fields

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form contains a field named "Email"
            if (!doc.Form.HasField("Email"))
            {
                Console.Error.WriteLine("The PDF does not contain an 'Email' field.");
                return;
            }

            // Retrieve the Email field and cast it to a Forms.Field
            Field emailField = doc.Form["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("The 'Email' field could not be accessed as a form field.");
                return;
            }

            // Create a JavaScript action that validates the presence of '@'
            string jsCode = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Please enter a valid email address.');
    event.rc = false; // Reject the input
}";
            JavascriptAction validateAction = new JavascriptAction(jsCode);

            // Assign the JavaScript to the field's OnValidate action
            emailField.Actions.OnValidate = validateAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email validation saved to '{outputPath}'.");
    }
}
