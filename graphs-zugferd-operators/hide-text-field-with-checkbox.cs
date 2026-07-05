using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create a checkbox field that will control visibility
            // -------------------------------------------------
            // Rectangle(left, bottom, width, height)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            CheckboxField chkHide = new CheckboxField(page, chkRect)
            {
                Name = "chkHide",               // field name (used for JavaScript)
                PartialName = "chkHide",
                // Set the export value when the box is checked
                ExportValue = "Yes",
                // Initially unchecked
                Checked = false
            };
            // Add the checkbox to the form
            doc.Form.Add(chkHide);

            // -------------------------------------------------
            // Create a text box field that will be hidden/shown
            // -------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(150, 700, 350, 720);
            TextBoxField txtTarget = new TextBoxField(page, txtRect)
            {
                Name = "txtTarget",
                PartialName = "txtTarget",
                // Provide some default text
                Value = "This field is visible when the checkbox is checked."
            };
            doc.Form.Add(txtTarget);

            // -------------------------------------------------
            // Attach JavaScript to hide/show the text field based on the checkbox state
            // -------------------------------------------------
            // The JavaScript checks the value of the checkbox ("Yes" when checked) and
            // sets the display property of the target field accordingly.
            string js = @"if (event.target.value == 'Yes')
                this.getField('txtTarget').display = display.visible;
            else
                this.getField('txtTarget').display = display.hidden;";

            // Use a valid action property from AnnotationActionCollection.
            // OnPressMouseBtn is triggered when the user clicks the checkbox.
            chkHide.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // -------------------------------------------------
            // Save the PDF
            // -------------------------------------------------
            const string outputPath = "CheckboxHideField.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
