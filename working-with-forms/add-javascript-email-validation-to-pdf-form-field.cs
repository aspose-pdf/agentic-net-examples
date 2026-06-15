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

        // Load the existing PDF (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the Email field will appear (left, bottom, width, height)
            Aspose.Pdf.Rectangle emailRect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a text box field for the email address
            TextBoxField emailField = new TextBoxField(doc, emailRect)
            {
                PartialName = "Email",          // field name
                Color = Color.LightGray,        // background / border color
                TextHorizontalAlignment = HorizontalAlignment.Left
            };

            // Set the border after the field instance is created (Border requires the parent annotation)
            emailField.Border = new Border(emailField) { Width = 1 };

            // JavaScript validation: ensure the value contains an '@' character
            string jsCode = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Please enter a valid email address.');
    event.rc = false; // reject the input
}";
            // Assign the JavaScript to the OnValidate action of the field
            emailField.Actions.OnValidate = new JavascriptAction(jsCode);

            // Add the field to the first page of the document
            doc.Form.Add(emailField, 1);

            // Save the modified PDF (using the same Document instance)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with email validation: '{outputPath}'");
    }
}
