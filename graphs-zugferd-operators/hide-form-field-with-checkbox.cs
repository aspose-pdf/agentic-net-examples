using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_hide_field.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create the checkbox that will control visibility
            // -------------------------------------------------
            // Rectangle: left, bottom, right, top
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            CheckboxField chkHide = new CheckboxField(page, chkRect);
            chkHide.Name = "chkHide";               // field name used in JavaScript
            chkHide.AlternateName = "Show/Hide Field";
            chkHide.Checked = false;                // initially unchecked
            doc.Form.Add(chkHide);

            // -------------------------------------------------
            // Create the target field that will be hidden/shown
            // -------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField txtTarget = new TextBoxField(page, txtRect);
            txtTarget.Name = "txtTarget";
            txtTarget.AlternateName = "Target Text Field";
            txtTarget.Contents = "This field is hidden when the box is unchecked.";
            doc.Form.Add(txtTarget);

            // -------------------------------------------------
            // Attach JavaScript to the checkbox to toggle visibility
            // -------------------------------------------------
            // The script checks the checkbox value; "Off" means unchecked.
            // It sets the display property of the target field accordingly.
            string js = @"
if (event.target.value == 'Off') {
    this.getField('txtTarget').display = display.hidden;
} else {
    this.getField('txtTarget').display = display.visible;
}";
            // Assign the script to a valid action property (OnCalculate) of the checkbox
            chkHide.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
