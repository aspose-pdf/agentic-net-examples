using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "conditional_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a checkbox that will control visibility
            // -------------------------------------------------
            CheckboxField checkBox = new CheckboxField(page, new Rectangle(50, 750, 70, 770))
            {
                // Field identifiers
                Name = "showDetails",
                PartialName = "showDetails",
                // Initial state: unchecked
                Checked = false,
                ExportValue = "Yes"
            };

            // JavaScript to show/hide the "details" field based on the checkbox state
            string js = @"
if (event.target.checked) {
    this.getField('details').display = display.visible;
} else {
    this.getField('details').display = display.hidden;
}";
            checkBox.OnActivated = new JavascriptAction(js);

            // Add the checkbox to the form
            doc.Form.Add(checkBox);

            // -------------------------------------------------
            // 2. Create a text field that will be conditionally shown
            // -------------------------------------------------
            TextBoxField detailsField = new TextBoxField(page, new Rectangle(50, 700, 300, 730))
            {
                Name = "details",
                PartialName = "details",
                Contents = "Additional information appears here."
            };

            // Hide the field initially using a document‑open JavaScript action
            // (the Display property is not available on TextBoxField in some Aspose.Pdf versions)
            page.Actions.OnOpen = new JavascriptAction("this.getField('details').display = display.hidden;");

            // Add the text field to the form
            doc.Form.Add(detailsField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
