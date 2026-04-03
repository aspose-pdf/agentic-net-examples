using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "collapsible_form.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Create the collapsible section (a text box field)
            // ------------------------------------------------------------
            // Rectangle: left, bottom, width, height
            Aspose.Pdf.Rectangle sectionRect = new Aspose.Pdf.Rectangle(50, 500, 400, 150);
            TextBoxField sectionField = new TextBoxField(page, sectionRect)
            {
                PartialName = "section",                     // field name used in JavaScript
                Contents   = "This is the collapsible content.\nIt becomes visible when the checkbox is checked."
                // NOTE: The 'Display' property is not available in the current Aspose.PDF version.
                // The field will be hidden initially via a document‑level JavaScript action (see below).
            };
            // Add the field to the form
            doc.Form.Add(sectionField);

            // ------------------------------------------------------------
            // 2. Create the checkbox that controls the section
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 450, 20, 20);
            CheckboxField chkField = new CheckboxField(page, chkRect)
            {
                PartialName = "showSection",   // field name
                ExportValue = "Yes",
                Checked     = false
            };

            // JavaScript to toggle visibility of the collapsible section
            string toggleJs = @"
                var f = this.getField('section');
                if (event.target.checked) {
                    f.display = display.visible;
                } else {
                    f.display = display.hidden;
                }";
            chkField.OnActivated = new JavascriptAction(toggleJs);

            // Add the checkbox to the form
            doc.Form.Add(chkField);

            // ------------------------------------------------------------
            // 3. Hide the collapsible section initially (document‑level JS)
            // ------------------------------------------------------------
            string initJs = "var f = this.getField('section'); f.display = display.hidden;";
            doc.OpenAction = new JavascriptAction(initJs);

            // ------------------------------------------------------------
            // Save the PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with collapsible section saved to '{outputPath}'.");
    }
}
