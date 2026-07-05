using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithAnalytics.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (first page is index 1)
            Page page = doc.Pages.Add();

            // Define a default appearance for form fields (Helvetica, 12pt, black)
            DefaultAppearance fieldAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // -----------------------------------------------------------------
            // 1. Create a hidden log field (zero‑size, read‑only)
            // -----------------------------------------------------------------
            // Position rectangle with zero width/height makes the field invisible.
            Aspose.Pdf.Rectangle logRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField logField = new TextBoxField(page, logRect)
            {
                Name = "Log",               // Full field name
                PartialName = "Log",
                ReadOnly = true,            // Prevent user editing
                DefaultAppearance = fieldAppearance
            };
            // Add the hidden log field to the document form
            doc.Form.Add(logField);

            // -----------------------------------------------------------------
            // 2. Create a sample visible text field
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                Name = "Name",
                PartialName = "Name",
                DefaultAppearance = fieldAppearance
            };
            // Attach JavaScript that appends an entry to the hidden log field
            // when the field receives focus (or is changed).
            // The script uses the Acrobat JavaScript API.
            string jsCode = @"
                var log = this.getField('Log');
                if (log) {
                    log.value += 'Name field focused at ' + new Date().toISOString() + '\n';
                }
            ";
            nameField.OnActivated = new JavascriptAction(jsCode);
            doc.Form.Add(nameField);

            // -----------------------------------------------------------------
            // 3. Create another sample field (e.g., a checkbox)
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle agreeRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);
            CheckboxField agreeField = new CheckboxField(page, agreeRect)
            {
                Name = "Agree",
                PartialName = "Agree",
                DefaultAppearance = fieldAppearance
            };
            // JavaScript to log checkbox interaction
            string jsCheck = @"
                var log = this.getField('Log');
                if (log) {
                    log.value += 'Agree checkbox toggled at ' + new Date().toISOString() + '\n';
                }
            ";
            agreeField.OnActivated = new JavascriptAction(jsCheck);
            doc.Form.Add(agreeField);

            // -----------------------------------------------------------------
            // Save the PDF document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with analytics saved to '{outputPath}'.");
    }
}