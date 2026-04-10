using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_hide_field.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create the field that will be hidden/shown
            // -------------------------------------------------
            Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
            TextBoxField targetField = new TextBoxField(page, targetRect);
            targetField.Name = "TargetField";               // field name used in JavaScript
            targetField.Value = "Visible text";
            targetField.Color = Color.LightGray;               // background color
            // Set border after the field is instantiated (Border requires the parent annotation)
            targetField.Border = new Border(targetField) { Width = 1 };
            doc.Form.Add(targetField);

            // -------------------------------------------------
            // Create the checkbox that controls visibility
            // -------------------------------------------------
            Aspose.Pdf.Rectangle checkRect = new Aspose.Pdf.Rectangle(50, 500, 70, 520);
            CheckboxField checkBox = new CheckboxField(page, checkRect);
            checkBox.Name = "ShowHideCheck";
            // Set initial state (unchecked)
            checkBox.Checked = false;
            checkBox.Color = Color.White;                     // background color
            // Set border after the field is instantiated
            checkBox.Border = new Border(checkBox) { Width = 1 };
            doc.Form.Add(checkBox);

            // -------------------------------------------------
            // Add JavaScript to the checkbox's OnCalculate action
            // -------------------------------------------------
            // The script hides the target field when the checkbox is unchecked (value "Off")
            // and shows it when checked (value "Yes").
            string jsCode = @"
if (event.target.value == 'Off') {
    this.getField('TargetField').display = display.hidden;
} else {
    this.getField('TargetField').display = display.visible;
}";
            // Use a valid action property for value‑change events (OnCalculate works for checkboxes)
            checkBox.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
