using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_hide_field.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create the checkbox that will control visibility
            // -------------------------------------------------
            // Fully qualified Rectangle to avoid ambiguity (rectangle-disambiguation rule)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            CheckboxField chk = new CheckboxField(page, chkRect)
            {
                Name = "ShowDetails",   // field name used in JavaScript
                Checked = false,        // start unchecked (field will be hidden)
                ExportValue = "Yes"     // value when the box is checked
            };
            doc.Form.Add(chk); // add the checkbox to the form

            // -------------------------------------------------
            // 2. Create the target field that will be hidden/shown
            // -------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(150, 700, 350, 720);
            TextBoxField txt = new TextBoxField(page, txtRect)
            {
                Name = "Details",               // field name referenced in JavaScript
                Value = "Additional information"
            };
            doc.Form.Add(txt); // add the text box to the form

            // -------------------------------------------------
            // 3. Attach JavaScript to the checkbox
            // -------------------------------------------------
            // The script runs when the user releases the mouse button on the checkbox (OnReleaseMouseBtn action)
            // It checks the checkbox state and sets the display property of the target field.
            string js = @"
if (event.target.value == 'Off')
{
    this.getField('Details').display = display.hidden;
}
else
{
    this.getField('Details').display = display.visible;
}";
            // Assign the JavaScript action using a valid action property (OnReleaseMouseBtn)
            chk.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Save the PDF (save without explicit SaveOptions writes PDF)
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
