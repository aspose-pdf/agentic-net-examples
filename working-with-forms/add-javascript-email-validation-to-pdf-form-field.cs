using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Email"
            // The Form collection can be accessed via doc.Form["fieldName"]
            if (doc.Form["Email"] is TextBoxField emailField)
            {
                // JavaScript that validates the presence of '@' in the field value
                // The script runs when the field is validated (e.g., on blur)
                JavascriptAction validateJs = new JavascriptAction(
                    "if (event.value.indexOf('@') == -1) { " +
                    "app.alert('Please enter a valid email address.'); " +
                    "event.rc = false; " + // reject the value
                    "}"
                );

                // Assign the JavaScript to the field's OnValidate action
                emailField.Actions.OnValidate = validateJs;
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain a TextBoxField named 'Email'.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript validation to '{outputPath}'.");
    }
}