using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using System.Text.Json; // for JSON serialization
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // needed for DefaultAppearance

class Program
{
    static void Main()
    {
        const string templatePath = "form_template.pdf";
        const string jsonPath     = "data.json";
        const string outputPath   = "filled_form.pdf";

        // ---------- Create a PDF form template ----------
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ----- Text box field (placeholder) -----
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField txtField = new TextBoxField(page, txtRect)
            {
                PartialName = "NameField",          // field identifier
                Value       = "Enter name"          // placeholder text
            };
            // Set default appearance using the constructor (font name, size, color)
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            // Add the field to the document's form (not the page)
            doc.Form.Add(txtField);

            // ----- Checkbox field (placeholder) -----
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 560, 115, 575);
            CheckboxField chkField = new CheckboxField(page, chkRect)
            {
                PartialName = "SubscribeField",
                Value       = "Off"                 // default unchecked
            };
            // Add the checkbox to the document's form
            doc.Form.Add(chkField);

            // Save the blank form template
            doc.Save(templatePath);
        }

        // ---------- Create a JSON data source matching the field names ----------
        var formData = new
        {
            NameField = "John Doe",
            SubscribeField = "On" // "On" checks the box, "Off" leaves it unchecked
        };
        string jsonString = JsonSerializer.Serialize(formData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, jsonString);

        // ---------- Populate the form from the JSON data source ----------
        using (Document doc = new Document(templatePath))
        {
            // Import field values from the JSON file (matches field names)
            doc.Form.ImportFromJson(jsonPath);

            // Optional: flatten the form if you want a non‑editable result
            // doc.Form.Flatten();

            // Save the filled PDF
            doc.Save(outputPath);
        }
    }
}
