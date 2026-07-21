using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm via the Document.Form property (no constructor needed)
            Form form = doc.Form;

            // Retrieve the field named "Email"
            // The indexer returns a generic Field; cast to TextBoxField for text fields
            if (form["Email"] is TextBoxField emailField)
            {
                // JavaScript to validate email format on blur (loss of focus) event
                string js = @"
var email = event.value;
if (!/^[\w\.-]+@[\w\.-]+\.[A-Za-z]{2,}$/.test(email)) {
    app.alert('Invalid email address');
    event.rc = false;
}";
                // Assign the script to the OnLostFocus action of the field (blur equivalent)
                emailField.Actions.OnLostFocus = new JavascriptAction(js);
            }
            else
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a text field.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript validation to '{outputPath}'.");
    }
}
