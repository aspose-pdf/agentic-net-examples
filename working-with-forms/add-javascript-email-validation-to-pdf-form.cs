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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the existing field named "Email"
            // Document.Form indexer returns a WidgetAnnotation, so cast it to Field
            Field emailField = doc.Form["Email"] as Field;
            if (emailField == null)
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a form field in the document.");
                return;
            }

            // Attach JavaScript validation that checks for the presence of '@'
            string jsCode = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Please enter a valid email address containing ''@''.');
    event.rc = false; // Reject the input
}";
            emailField.Actions.OnValidate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with email validation: '{outputPath}'.");
    }
}
