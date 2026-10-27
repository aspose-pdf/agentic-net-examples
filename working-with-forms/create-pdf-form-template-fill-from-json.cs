using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for form field annotations
using Aspose.Pdf.Forms;      // for AcroForm field types
using Aspose.Pdf.Text;       // for TextFragment and Position

class Program
{
    // Simple POCO matching the expected JSON structure
    private class FormData
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Subscribe { get; set; }
    }

    static void Main()
    {
        const string templatePath = "form_template.pdf";
        const string outputPath   = "filled_form.pdf";
        const string jsonPath     = "data.json";

        // -------------------------------------------------
        // Step 1: Create a PDF form template with placeholder fields
        // -------------------------------------------------
        if (!File.Exists(templatePath))
        {
            using (Document doc = new Document())
            {
                // Add a single page
                Page page = doc.Pages.Add();

                // ---- TextBox for Name ----
                // Rectangle(left, bottom, right, top) – coordinates are in points
                Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
                TextBoxField nameField = new TextBoxField(page, nameRect)
                {
                    PartialName = "Name",
                    Value       = "Enter your name"
                };
                doc.Form.Add(nameField);

                // ---- TextBox for Email ----
                Aspose.Pdf.Rectangle emailRect = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
                TextBoxField emailField = new TextBoxField(page, emailRect)
                {
                    PartialName = "Email",
                    Value       = "Enter your email"
                };
                doc.Form.Add(emailField);

                // ---- CheckBox for Subscription ----
                Aspose.Pdf.Rectangle subscribeRect = new Aspose.Pdf.Rectangle(100, 600, 115, 615);
                CheckboxField subscribeField = new CheckboxField(page, subscribeRect)
                {
                    PartialName = "Subscribe",
                    // Default unchecked; will set Checked = true/false later
                };
                doc.Form.Add(subscribeField);

                // Optional: add visual labels (simple text fragments)
                TextFragment nameLabel = new TextFragment("Name:")
                {
                    Position = new Position(20, 710),
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(nameLabel);

                TextFragment emailLabel = new TextFragment("Email:")
                {
                    Position = new Position(20, 660),
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(emailLabel);

                TextFragment subscribeLabel = new TextFragment("Subscribe to newsletter:")
                {
                    Position = new Position(120, 610),
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(subscribeLabel);

                // Save the template
                doc.Save(templatePath);
                Console.WriteLine($"Template created at '{templatePath}'.");
            }
        }

        // -------------------------------------------------
        // Step 2: Load JSON data and populate the form fields
        // -------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into the FormData object
        FormData data;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            data = JsonSerializer.Deserialize<FormData>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Open the template, fill fields, and save the result
        using (Document doc = new Document(templatePath))
        {
            // Set text box values
            if (doc.Form["Name"] is TextBoxField nameField)
                nameField.Value = data.Name ?? string.Empty;

            if (doc.Form["Email"] is TextBoxField emailField)
                emailField.Value = data.Email ?? string.Empty;

            // Set checkbox state – use the Checked property (bool)
            if (doc.Form["Subscribe"] is CheckboxField subscribeField)
                subscribeField.Checked = data.Subscribe;

            // Save the filled PDF
            doc.Save(outputPath);
            Console.WriteLine($"Filled form saved at '{outputPath}'.");
        }
    }
}
