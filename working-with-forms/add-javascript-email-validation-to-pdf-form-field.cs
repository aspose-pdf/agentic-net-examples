using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_validated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form and retrieve the 'Email' field (cast to Forms.Field)
            Field emailField = doc.Form["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a form field.");
                return;
            }

            // JavaScript that validates the presence of '@' in the field value
            string jsCode = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Please enter a valid email address.');
    event.rc = false; // reject the change
}";

            // Assign the JavaScript to the field's OnValidate action
            emailField.Actions.OnValidate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with validation script: {outputPath}");
    }
}
