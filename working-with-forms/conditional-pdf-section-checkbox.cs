using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction, AnnotationFlags
using Aspose.Pdf.Forms;      // Form field types

class Program
{
    static void Main()
    {
        const string outputPath = "ConditionalForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the form fields
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Add a checkbox that will control the visibility
            // -------------------------------------------------
            CheckboxField checkBox = new CheckboxField(page, new Rectangle(100, 700, 120, 720));
            checkBox.PartialName = "ShowSection";   // field name used in JavaScript
            checkBox.Checked = true;                 // initial state (checked)
            // NOTE: In recent Aspose.PDF versions the export values are "Yes"/"Off" by default,
            // so explicit OnState/OffState assignments are unnecessary and the properties no longer exist.
            doc.Form.Add(checkBox);

            // -------------------------------------------------
            // 2. Add a text field that will be shown/hidden
            // -------------------------------------------------
            TextBoxField conditionalField = new TextBoxField(page, new Rectangle(100, 650, 300, 680));
            conditionalField.PartialName = "ConditionalText";
            conditionalField.Value = "This text appears only when the checkbox is checked.";
            // Initial visibility: hidden
            conditionalField.Flags = AnnotationFlags.Hidden;
            doc.Form.Add(conditionalField);

            // -------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility
            // -------------------------------------------------
            string js = @"
if (this.getField('ShowSection').value == 'Yes') {
    this.getField('ConditionalText').display = display.visible;
} else {
    this.getField('ConditionalText').display = display.hidden;
}";
            // Assign the script to the press‑mouse‑button action (valid for CheckboxField)
            checkBox.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with conditional section saved to '{outputPath}'.");
    }
}
