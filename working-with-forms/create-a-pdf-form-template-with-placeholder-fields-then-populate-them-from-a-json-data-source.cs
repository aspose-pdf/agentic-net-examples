using System;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string outputPath   = "PopulatedForm.pdf";

        // -------------------------------------------------
        // 1. Create a PDF form template with placeholder fields
        // -------------------------------------------------
        using (Document templateDoc = new Document())
        {
            // Add a single page to the document
            Page page = templateDoc.Pages.Add();

            // Define rectangles for the form fields (left, bottom, right, top)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle nameFieldRect  = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            Aspose.Pdf.Rectangle emailFieldRect = new Aspose.Pdf.Rectangle(100, 650, 300, 670);

            // Create a text box field for "Name"
            TextBoxField nameField = new TextBoxField(page, nameFieldRect);
            nameField.PartialName = "Name";
            nameField.Value = "Enter name here";

            // Create a text box field for "Email"
            TextBoxField emailField = new TextBoxField(page, emailFieldRect);
            emailField.PartialName = "Email";
            emailField.Value = "Enter email here";

            // Add the fields to the form (explicit for clarity)
            templateDoc.Form.Add(nameField);
            templateDoc.Form.Add(emailField);

            // Save the template PDF
            templateDoc.Save(templatePath);
        }

        // -------------------------------------------------
        // 2. Load the template and populate fields from JSON
        // -------------------------------------------------
        // JSON data that would normally be read from a file.
        string jsonData = "{\"Name\":\"John Doe\",\"Email\":\"john.doe@example.com\"}";

        using (Document doc = new Document(templatePath))
        {
            // Parse the JSON string
            using JsonDocument jsonDoc = JsonDocument.Parse(jsonData);
            JsonElement root = jsonDoc.RootElement;

            // Iterate over all form fields and set values that exist in the JSON
            foreach (Field field in doc.Form)
            {
                if (field is TextBoxField txtField)
                {
                    if (root.TryGetProperty(txtField.PartialName, out JsonElement valueElement))
                    {
                        txtField.Value = valueElement.GetString();
                    }
                }
            }

            // Save the populated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form populated and saved to '{outputPath}'.");
    }
}
