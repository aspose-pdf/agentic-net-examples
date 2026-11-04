using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string templatePath = "form_template.pdf";
        const string outputPath   = "filled_form.pdf";
        const string jsonPath     = "data.json";

        // -----------------------------------------------------------------
        // 1. Create a PDF form template with placeholder fields
        // -----------------------------------------------------------------
        using (Document templateDoc = new Document())
        {
            // Add a single page
            Page page = templateDoc.Pages.Add();

            // Define a common text style for field labels
            TextFragment labelStyle = new TextFragment();
            labelStyle.TextState.FontSize = 12;
            labelStyle.TextState.Font = FontRepository.FindFont("Helvetica");
            labelStyle.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // ---- TextBox field: Name ----
            // Label
            TextFragment nameLabel = (TextFragment)labelStyle.Clone();
            nameLabel.Text = "Name:";
            nameLabel.Position = new Position(50, 750);
            page.Paragraphs.Add(nameLabel);

            // TextBox field
            TextBoxField nameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(150, 735, 350, 755))
            {
                PartialName = "Name",
                Value = "Enter name here"
            };
            templateDoc.Form.Add(nameField, 1);

            // ---- TextBox field: Email ----
            TextFragment emailLabel = (TextFragment)labelStyle.Clone();
            emailLabel.Text = "Email:";
            emailLabel.Position = new Position(50, 700);
            page.Paragraphs.Add(emailLabel);

            TextBoxField emailField = new TextBoxField(page, new Aspose.Pdf.Rectangle(150, 685, 350, 705))
            {
                PartialName = "Email",
                Value = "Enter email here"
            };
            templateDoc.Form.Add(emailField, 1);

            // ---- CheckBox field: Subscribe ----
            TextFragment subLabel = (TextFragment)labelStyle.Clone();
            subLabel.Text = "Subscribe to newsletter:";
            subLabel.Position = new Position(50, 650);
            page.Paragraphs.Add(subLabel);

            CheckboxField subscribeField = new CheckboxField(page, new Aspose.Pdf.Rectangle(250, 635, 265, 650))
            {
                PartialName = "Subscribe",
                Checked = false,
                Style = BoxStyle.Check // optional visual style
            };
            templateDoc.Form.Add(subscribeField, 1);

            // Save the template
            templateDoc.Save(templatePath);
        }

        // -----------------------------------------------------------------
        // 2. Load JSON data source
        // -----------------------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        string jsonContent = File.ReadAllText(jsonPath);
        // Expecting a simple key/value JSON, e.g. {"Name":"John Doe","Email":"john@example.com","Subscribe":true}
        Dictionary<string, JsonElement> jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonContent);

        // -----------------------------------------------------------------
        // 3. Populate the template with JSON values
        // -----------------------------------------------------------------
        using (Document doc = new Document(templatePath))
        {
            foreach (var kvp in jsonData)
            {
                // Retrieve the field by its partial name – the indexer returns a Field (or null)
                Field field = doc.Form[kvp.Key] as Field;
                if (field == null)
                    continue; // Skip unknown or non‑field annotations

                switch (field)
                {
                    case TextBoxField txtField:
                        txtField.Value = kvp.Value.GetString() ?? string.Empty;
                        break;

                    case CheckboxField chkField:
                        bool isChecked = false;
                        if (kvp.Value.ValueKind == JsonValueKind.True)
                            isChecked = true;
                        else if (kvp.Value.ValueKind == JsonValueKind.False)
                            isChecked = false;
                        else if (kvp.Value.ValueKind == JsonValueKind.String)
                        {
                            string s = kvp.Value.GetString()?.ToLowerInvariant();
                            isChecked = s == "true" || s == "yes" || s == "1";
                        }
                        chkField.Checked = isChecked;
                        break;

                    default:
                        // Fallback for any other field types – try to set the generic Value property
                        field.Value = kvp.Value.GetString() ?? kvp.Value.ToString();
                        break;
                }
            }

            // Save the filled PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }
}
