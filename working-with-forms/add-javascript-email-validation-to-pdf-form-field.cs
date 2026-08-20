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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the 'Email' field from the form and cast to Field
            Field emailField = doc.Form["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("The form does not contain a field named 'Email' or it is not a form field.");
                return;
            }

            // JavaScript validation: ensure the value contains an '@' character
            string jsCode = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Please enter a valid email address.');
    event.rc = false; // reject the input
}";
            // Assign the JavaScript action to the field's OnValidate event
            emailField.Actions.OnValidate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with email validation to '{outputPath}'.");
    }
}
