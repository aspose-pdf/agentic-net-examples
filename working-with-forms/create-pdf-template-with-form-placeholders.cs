using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for DefaultAppearance
using System.Drawing; // required for System.Drawing.Color used by DefaultAppearance

class PdfTemplateCreator
{
    static void Main()
    {
        const string outputPath = "Template.pdf";

        // Create a new empty PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create placeholder text field "Name"
            // -------------------------------------------------
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                PartialName = "Name",
                Value = ""
            };
            doc.Form.Add(nameField);
            doc.Form.AddFieldAppearance(nameField, 1, nameRect);

            // -------------------------------------------------
            // Create placeholder checkbox field "Agree"
            // -------------------------------------------------
            Aspose.Pdf.Rectangle agreeRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);
            CheckboxField agreeField = new CheckboxField(page, agreeRect)
            {
                PartialName = "Agree",
                Value = "Off"
            };
            doc.Form.Add(agreeField);
            doc.Form.AddFieldAppearance(agreeField, 1, agreeRect);

            // -------------------------------------------------
            // Create placeholder date field "Date"
            // -------------------------------------------------
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(100, 600, 200, 630);
            TextBoxField dateField = new TextBoxField(page, dateRect)
            {
                PartialName = "Date",
                Value = ""
            };
            doc.Form.Add(dateField);
            doc.Form.AddFieldAppearance(dateField, 1, dateRect);

            // Set a default appearance for form fields (font, size, color)
            // The Form object is guaranteed to be instantiated after the first field is added.
            DefaultAppearance defaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.DefaultAppearance = defaultAppearance;

            // Save the template PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF template with placeholders saved to '{outputPath}'.");
    }
}
