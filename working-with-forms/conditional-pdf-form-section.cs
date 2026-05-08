using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ConditionalForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a checkbox that will control visibility
            // -------------------------------------------------
            // Position: (50, 750) lower‑left, (70, 770) upper‑right
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField showSectionChk = new CheckboxField(page, checkboxRect)
            {
                Name = "showSection",          // field name used in JavaScript
                PartialName = "showSection",
                ExportValue = "Yes",           // value when checked
                Checked = false                // initially unchecked
            };

            // JavaScript to toggle the visibility of the conditional field
            // The target field is named "conditionalText"
            string toggleJs = @"
if (event.target.checked) {
    this.getField('conditionalText').display = display.visible;
} else {
    this.getField('conditionalText').display = display.hidden;
}";
            showSectionChk.OnActivated = new JavascriptAction(toggleJs);

            // Add the checkbox to the form
            doc.Form.Add(showSectionChk);

            // -------------------------------------------------
            // 2. Create a text field that appears conditionally
            // -------------------------------------------------
            // Position: (50, 700) lower‑left, (300, 730) upper‑right
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(50, 700, 300, 730);
            TextBoxField conditionalText = new TextBoxField(page, textRect)
            {
                Name = "conditionalText",
                PartialName = "conditionalText",
                Contents = "Enter additional information here..."
                // No direct Display property used – visibility will be controlled via JavaScript
            };

            // Add the text field to the form
            doc.Form.Add(conditionalText);

            // -------------------------------------------------
            // 3. Hide the conditional field on document open (initial state)
            // -------------------------------------------------
            string initJs = "this.getField('conditionalText').display = display.hidden;";
            page.Actions.OnOpen = new JavascriptAction(initJs);

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with conditional section saved to '{outputPath}'.");
    }
}
