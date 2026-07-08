using System;
using System.IO;
using System.Drawing;                     // Required for DefaultAppearance constructor
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class PdfTemplateCreator
{
    static void Main()
    {
        const string outputPath = "template.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Define a default appearance for form fields
            // (font name, size, and color). The constructor
            // requires System.Drawing.Color as the third argument.
            // -------------------------------------------------
            DefaultAppearance fieldAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // -------------------------------------------------
            // Text box placeholder field
            // -------------------------------------------------
            TextBoxField nameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 730))
            {
                PartialName = "Name",               // Field identifier
                Value = "{{Name}}",                 // Placeholder text
                DefaultAppearance = fieldAppearance // Apply appearance
            };
            doc.Form.Add(nameField);               // Register field with the document form

            // -------------------------------------------------
            // Date placeholder field
            // -------------------------------------------------
            TextBoxField dateField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 300, 680))
            {
                PartialName = "Date",
                Value = "{{Date}}",
                DefaultAppearance = fieldAppearance
            };
            doc.Form.Add(dateField);

            // -------------------------------------------------
            // Checkbox placeholder field
            // -------------------------------------------------
            CheckboxField agreeField = new CheckboxField(page, new Aspose.Pdf.Rectangle(100, 600, 115, 615))
            {
                PartialName = "Agree",
                Value = "Off",                      // Default unchecked state
                DefaultAppearance = fieldAppearance
            };
            doc.Form.Add(agreeField);

            // -------------------------------------------------
            // Save the template PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF template with placeholders saved to '{outputPath}'.");
    }
}