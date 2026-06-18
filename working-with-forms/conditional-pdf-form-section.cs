using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "ConditionalForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Add a checkbox that will control the visibility of a section
            // -----------------------------------------------------------------
            // Position: left=100, bottom=700, right=120, top=720
            var checkboxRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            var checkBox = new CheckboxField(page, checkboxRect)
            {
                PartialName = "showDetails",   // field name used in JavaScript
                ExportValue = "Yes",           // value when checked
                Color = Aspose.Pdf.Color.Blue
            };
            // Border must be set after the field instance exists (cannot reference the variable inside its own initializer)
            checkBox.Border = new Border(checkBox) { Width = 1 };
            doc.Form.Add(checkBox);

            // -----------------------------------------------------------------
            // 2. Add a text field that will be shown/hidden based on the checkbox
            // -----------------------------------------------------------------
            // Position: left=100, bottom=650, right=300, top=680
            var textRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            var detailsField = new TextBoxField(page, textRect)
            {
                PartialName = "details",
                Color = Aspose.Pdf.Color.LightGray
            };
            detailsField.Border = new Border(detailsField) { Width = 1 };
            // Initially hide the field (set the Hidden flag). The Flags property expects an AnnotationFlags enum, not an int.
            detailsField.Flags = AnnotationFlags.Hidden;
            doc.Form.Add(detailsField);

            // -----------------------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility
            // -----------------------------------------------------------------
            string js = @"
if (event.target.value == 'Yes') {
    this.getField('details').display = display.visible;
} else {
    this.getField('details').display = display.hidden;
}";
            // Use a valid action property. OnCalculate is invoked when the field value changes.
            checkBox.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with conditional section saved to '{outputPath}'.");
    }
}