using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // for JavascriptAction, AnnotationFlags
using Aspose.Pdf.Forms;               // for form fields
using Aspose.Pdf.Text;                // for text fragments

class Program
{
    static void Main()
    {
        const string outputPath = "conditional_form.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Add a label for the checkbox.
            // -----------------------------------------------------------------
            TextFragment label = new TextFragment("Show additional section:");
            label.Position = new Position(50, 750);
            page.Paragraphs.Add(label);

            // -----------------------------------------------------------------
            // 2. Add the checkbox field.
            // -----------------------------------------------------------------
            // Rectangle is fully qualified to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle checkBoxRect = new Aspose.Pdf.Rectangle(250, 735, 270, 755);
            CheckboxField checkBox = new CheckboxField(page, checkBoxRect)
            {
                // Field name used in JavaScript.
                PartialName = "ShowSection",
                // Unchecked by default.
                Value = "Off"
            };
            doc.Form.Add(checkBox);

            // -----------------------------------------------------------------
            // 3. Add the conditional section (a simple text box) – hidden initially.
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle textBoxRect = new Aspose.Pdf.Rectangle(50, 650, 550, 720);
            TextBoxField conditionalBox = new TextBoxField(page, textBoxRect)
            {
                PartialName = "ConditionalSection",
                // Hide the field initially using annotation flags.
                Flags = AnnotationFlags.Hidden,
                // Optional default text.
                Value = "This text appears only when the checkbox is checked."
            };
            doc.Form.Add(conditionalBox);

            // -----------------------------------------------------------------
            // 4. Attach JavaScript to the checkbox to toggle visibility.
            // -----------------------------------------------------------------
            // The JavaScript checks the checkbox value and sets the display
            // property of the conditional field accordingly.
            string js = @"
if (this.getField('ShowSection').value == 'On') {
    this.getField('ConditionalSection').display = display.visible;
} else {
    this.getField('ConditionalSection').display = display.hidden;
}";
            // Assign the script to a valid action property (OnCalculate works for value changes).
            checkBox.Actions.OnCalculate = new JavascriptAction(js);

            // -----------------------------------------------------------------
            // 5. Save the PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with conditional section saved to '{outputPath}'.");
    }
}
