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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing for Pages collection)
            Page page = doc.Pages[1];

            // Define the rectangle where the Email field will be placed.
            // Use Aspose.Pdf.Rectangle (not Aspose.Pdf.Drawing.Rectangle) for form fields.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field bound to the page
            TextBoxField emailField = new TextBoxField(page, fieldRect)
            {
                PartialName = "Email" // Field name
            };

            // JavaScript that validates the presence of '@' in the entered value
            string jsCode = @"\nif (event.value.indexOf('@') == -1) {\n    app.alert('Please enter a valid email address.');\n    event.rc = false; // Reject the input\n}\n";
            JavascriptAction validateAction = new JavascriptAction(jsCode);

            // Assign the validation script to the field's OnValidate action
            emailField.Actions.OnValidate = validateAction;

            // Add the field to the form on page 1 (Document.Form.Add expects a page number, not a Page object)
            doc.Form.Add(emailField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Email validation: {outputPath}");
    }
}
