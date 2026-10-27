using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "CollapsibleForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // ----- Checkbox field (the trigger) -----
            // Define the rectangle for the checkbox (lower‑left x,y and upper‑right x,y)
            var chkRect = new Rectangle(100, 700, 120, 720);
            var chk = new CheckboxField(page, chkRect)
            {
                PartialName = "toggle",   // field name
                // OnState and OffState are not required – the default values ("On"/"Off") are sufficient
                Value = "Off"              // start unchecked
            };

            // JavaScript that toggles visibility of the collapsible field
            string js = @"
var f = this.getField('collapsible');
if (event.target.value == 'On') {
    f.display = display.visible;
} else {
    f.display = display.hidden;
}";
            // Use the OnCalculate action – it fires when the field value changes
            chk.Actions.OnCalculate = new JavascriptAction(js);

            // Add the checkbox to the form on page 1
            doc.Form.Add(chk, 1);

            // ----- Collapsible content (initially hidden) -----
            // Define rectangle for the hidden text box
            var txtRect = new Rectangle(100, 600, 400, 680);
            var txt = new TextBoxField(page, txtRect)
            {
                PartialName = "collapsible", // field name referenced in JavaScript
                Value = "This is the hidden content that becomes visible when the checkbox is checked."
            };

            // Hide the field initially using the Hidden annotation flag
            txt.Flags = AnnotationFlags.Hidden;

            // Add the hidden text box to the form on page 1
            doc.Form.Add(txt, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form saved to '{outputPath}'.");
    }
}
