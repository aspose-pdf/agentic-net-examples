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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the TextBox field named "Email"
            TextBoxField emailField = doc.Form["Email"] as TextBoxField;
            if (emailField == null)
            {
                Console.Error.WriteLine("Field 'Email' not found or is not a TextBoxField.");
                return;
            }

            // JavaScript that validates the email format on blur (OnLostFocus)
            string js = @"
var f = this.getField('Email');
var val = f.value;
var pattern = /^[\w\-\.+]+@([\w\-]+\.)+[\w\-]{2,4}$/;
if (!pattern.test(val)) {
    app.alert('Invalid email address');
    f.setFocus();
}
";

            // Assign the JavaScript action to the OnLostFocus event
            emailField.Actions.OnLostFocus = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript validation to '{outputPath}'.");
    }
}
