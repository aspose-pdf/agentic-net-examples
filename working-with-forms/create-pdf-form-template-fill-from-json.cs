using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // File paths
        const string templatePath = "template.pdf";
        const string jsonPath     = "data.json";
        const string filledPath   = "filled.pdf";

        // -------------------------------------------------
        // 1. Create a PDF form template with placeholder fields
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a new page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ----- Text box field (Name) -----
            // Rectangle(left, bottom, width, height)
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                PartialName = "Name",          // field identifier
                Value       = "Enter name"    // placeholder text
            };
            doc.Form.Add(nameField);           // attach to the form (adds annotation)

            // ----- Check box field (Subscribe) -----
            Aspose.Pdf.Rectangle subscribeRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);
            CheckboxField subscribeField = new CheckboxField(page, subscribeRect)
            {
                PartialName = "Subscribe",
                Value       = "Off"           // default unchecked
            };
            doc.Form.Add(subscribeField);

            // Save the empty template
            doc.Save(templatePath);
        }

        // -------------------------------------------------
        // 2. Prepare JSON data matching the field names
        // -------------------------------------------------
        // Example JSON: { "Name":"John Doe", "Subscribe":true }
        string jsonContent = @"{ ""Name"": ""John Doe"", ""Subscribe"": true }";
        File.WriteAllText(jsonPath, jsonContent);

        // -------------------------------------------------
        // 3. Load the template and populate fields from JSON
        // -------------------------------------------------
        using (Document doc = new Document(templatePath))
        {
            // Import field values from the JSON file
            doc.Form.ImportFromJson(jsonPath);

            // Save the filled PDF
            doc.Save(filledPath);
        }
    }
}