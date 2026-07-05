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
        const string outputPath = "output_validated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form exists
            Form form = doc.Form;

            // Try to get the existing 'Email' field; if it does not exist, create one
            Field emailField = doc.Form["Email"] as Field;
            if (emailField == null)
            {
                // Define the rectangle where the field will be placed (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

                // Create a new text box field on page 1
                TextBoxField newField = new TextBoxField(doc, rect);
                newField.PartialName = "Email";
                newField.Color = Aspose.Pdf.Color.LightGray;
                // Set the border after the field instance is created (avoid self‑reference in initializer)
                newField.Border = new Border(newField) { Width = 1 };

                // Add the field to the form on page 1
                form.Add(newField, 1);
                emailField = newField;
            }

            // Attach JavaScript validation that checks for the presence of '@'
            // The script runs when the field is validated (e.g., when the user leaves the field)
            string jsCode = @"
if (event.value.indexOf('@') == -1) {
    app.alert('Please enter a valid email address.');
    event.rc = false; // Reject the value
}";
            // Set the OnValidate action using a JavascriptAction
            emailField.Actions.OnValidate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email validation saved to '{outputPath}'.");
    }
}
