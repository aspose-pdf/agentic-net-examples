using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    // Simple DTO that matches the form field names
    private class FormData
    {
        public string FullName { get; set; }
        public string Subscribe { get; set; }
    }

    static void Main()
    {
        const string templatePath = "form_template.pdf";
        const string jsonPath     = "data.json";
        const string outputPath   = "filled_form.pdf";

        // -------------------------------------------------
        // 1. Create a PDF form template with placeholder fields
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ----- Text box field (e.g., FullName) -----
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            TextBoxField nameField = new TextBoxField(page, nameRect);
            nameField.PartialName = "FullName";                     // field identifier
            nameField.Value = "Enter full name";                    // placeholder text
            // Set default appearance (font, size, color) via constructor
            nameField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(nameField);

            // ----- Check box field (e.g., Subscribe) -----
            Aspose.Pdf.Rectangle checkRect = new Aspose.Pdf.Rectangle(100, 650, 115, 665);
            CheckboxField subscribeField = new CheckboxField(page, checkRect);
            subscribeField.PartialName = "Subscribe";
            subscribeField.Value = "Off"; // default unchecked
            doc.Form.Add(subscribeField);

            // Save the template (optional, can be omitted if not needed on disk)
            doc.Save(templatePath);
        }

        // -------------------------------------------------
        // 2. Ensure a JSON data source exists (create a sample if missing)
        // -------------------------------------------------
        if (!File.Exists(jsonPath))
        {
            var sampleData = new FormData
            {
                FullName = "John Doe",
                // For a checkbox Aspose expects "On" (checked) or "Off" (unchecked)
                Subscribe = "On"
            };
            string json = JsonSerializer.Serialize(sampleData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, json);
        }

        // -------------------------------------------------
        // 3. Load the template and populate fields from JSON
        // -------------------------------------------------
        using (Document doc = new Document(templatePath))
        {
            // Import field values from a JSON file.
            // The JSON should contain keys matching the field PartialName values.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the filled PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Form template created and populated from JSON successfully.");
    }
}
