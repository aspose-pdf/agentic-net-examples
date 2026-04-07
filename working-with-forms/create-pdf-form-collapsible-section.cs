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

            // Define the rectangle for the checkbox (left, bottom, right, top)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

            // Create a checkbox field
            CheckboxField chk = new CheckboxField(page, chkRect)
            {
                // Name used to reference the field in JavaScript
                Name = "ShowSection",
                // Export value when the box is checked
                ExportValue = "Yes",
                // Tooltip (alternate name)
                AlternateName = "Show Section"
            };

            // JavaScript to toggle visibility of the collapsible section
            // The target field is named "CollapsibleSection"
            string js = @"
var f = this.getField('CollapsibleSection');
if (event.target.value == 'Yes')
    f.display = display.visible;
else
    f.display = display.hidden;
";
            // Assign the JavaScript to the checkbox activation event
            chk.OnActivated = new JavascriptAction(js);

            // Add the checkbox to the form
            doc.Form.Add(chk);

            // Define the rectangle for the collapsible content (e.g., a text field)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 650, 550, 730);

            // Create a text box field that will act as the collapsible section
            TextBoxField txt = new TextBoxField(page, txtRect)
            {
                Name = "CollapsibleSection",
                // Initially hidden
                Flags = AnnotationFlags.Hidden,
                // Optional default text
                Contents = "This is the collapsible content that appears when the checkbox is checked."
            };

            // Add the text box to the form
            doc.Form.Add(txt);

            // Save the PDF document
            doc.Save("CollapsibleForm.pdf");
        }

        Console.WriteLine("PDF form with collapsible section created successfully.");
    }
}