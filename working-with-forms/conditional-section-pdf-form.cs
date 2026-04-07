using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // needed for TextFragment and Position

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
            // 1. Add a checkbox that will control the section
            // -------------------------------------------------
            // Position and size of the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField showDetails = new CheckboxField(page, checkboxRect)
            {
                PartialName = "showDetails",          // field name
                ExportValue = "Yes",                 // value when checked
                Color = Aspose.Pdf.Color.Blue         // optional visual setting
            };
            // Border must be set after the object is created (cannot reference it inside initializer)
            showDetails.Border = new Border(showDetails) { Width = 1 };

            // Add JavaScript to toggle visibility of the target field
            showDetails.OnActivated = new JavascriptAction(
                "var f = this.getField('details');" +
                "if (event.target.checked) { f.display = display.visible; } else { f.display = display.hidden; }"
            );

            // Add the checkbox to the form
            doc.Form.Add(showDetails);

            // -------------------------------------------------
            // 2. Add a text field that will be shown/hidden
            // -------------------------------------------------
            // Position and size of the text field
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 400, 730);
            TextBoxField detailsField = new TextBoxField(page, textRect)
            {
                PartialName = "details",
                ReadOnly = true,
                Color = Aspose.Pdf.Color.LightGray
                // No Display property – visibility will be controlled via JavaScript
            };
            detailsField.Border = new Border(detailsField) { Width = 1 };

            // Add the text field to the form
            doc.Form.Add(detailsField);

            // Hide the details field initially when the document is opened
            doc.OpenAction = new JavascriptAction("var f = this.getField('details'); f.display = display.hidden;");

            // -------------------------------------------------
            // 3. Add some labels for clarity (optional)
            // -------------------------------------------------
            // Label for the checkbox
            TextFragment cbLabel = new TextFragment("Show details:");
            cbLabel.Position = new Position(50, 770);
            page.Paragraphs.Add(cbLabel);

            // Label for the details field
            TextFragment detailsLabel = new TextFragment("Details (visible when checked):");
            detailsLabel.Position = new Position(100, 730);
            page.Paragraphs.Add(detailsLabel);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with conditional section saved to '{outputPath}'.");
    }
}
