using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "collapsible_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Checkbox that will control the collapsible section
            // -----------------------------------------------------------------
            // Rectangle for the checkbox (left, bottom, right, top)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField chk = new CheckboxField(page, chkRect)
            {
                Name = "ShowDetails",          // field name used in JavaScript
                AlternateName = "Show Details",// tooltip
                Checked = false                // start unchecked
            };
            doc.Form.Add(chk);

            // -----------------------------------------------------------------
            // 2. Text field that will be shown/hidden
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 650, 300, 730);
            TextBoxField txt = new TextBoxField(page, txtRect)
            {
                Name = "Details",
                Contents = "This is the hidden details section.\nIt becomes visible when the checkbox is checked.",
                Multiline = true
                // Do NOT set IsHidden here – the field will be hidden initially via JavaScript.
            };
            doc.Form.Add(txt);

            // -----------------------------------------------------------------
            // 3. JavaScript action attached to the checkbox
            // -----------------------------------------------------------------
            // The script toggles the visibility of the "Details" field and also hides it on load.
            string js = @"
                var f = this.getField('Details');
                // Ensure the field starts hidden when the document is opened
                if (event.type === 'Doc') {
                    f.display = display.hidden;
                }
                if (event.target.checked) {
                    f.display = display.visible;
                } else {
                    f.display = display.hidden;
                }
            ";
            chk.OnActivated = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
