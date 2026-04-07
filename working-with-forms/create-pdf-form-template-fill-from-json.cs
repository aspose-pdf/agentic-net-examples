using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string templatePath = "FormTemplate.pdf";
        const string jsonPath = "data.json";
        const string filledPath = "FormFilled.pdf";

        // Create a PDF form template with placeholder fields
        CreateFormTemplate(templatePath);

        // Populate the form fields from a JSON data source
        PopulateFormFromJson(templatePath, jsonPath, filledPath);
    }

    static void CreateFormTemplate(string outputPath)
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Text box for "NameField"
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                PartialName = "NameField",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
                Value = "Enter name"
            };
            doc.Form.Add(nameField);

            // Text box for "EmailField"
            Aspose.Pdf.Rectangle emailRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField emailField = new TextBoxField(page, emailRect)
            {
                PartialName = "EmailField",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
                Value = "Enter email"
            };
            doc.Form.Add(emailField);

            // Checkbox for "Subscribe"
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 600, 115, 615);
            CheckboxField subscribeField = new CheckboxField(page, chkRect)
            {
                PartialName = "Subscribe",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };
            doc.Form.Add(subscribeField);

            // Save the template PDF
            doc.Save(outputPath);
        }
    }

    static void PopulateFormFromJson(string templatePath, string jsonPath, string outputPath)
    {
        // If the JSON file does not exist, create a simple example
        if (!File.Exists(jsonPath))
        {
            string sampleJson = @"{
  ""NameField"": ""John Doe"",
  ""EmailField"": ""john.doe@example.com"",
  ""Subscribe"": true
}";
            File.WriteAllText(jsonPath, sampleJson);
        }

        // Load the PDF template
        using (Document doc = new Document(templatePath))
        {
            // Import field values from the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Save the filled PDF
            doc.Save(outputPath);
        }
    }
}