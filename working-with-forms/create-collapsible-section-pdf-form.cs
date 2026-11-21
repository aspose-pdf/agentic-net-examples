using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "CollapsibleForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // 1. Add a checkbox that will control the collapsible section.
            // ------------------------------------------------------------
            CheckboxField checkBox = new CheckboxField(page,
                new Aspose.Pdf.Rectangle(100, 750, 120, 770));
            checkBox.PartialName = "ShowSection";
            checkBox.Checked = false; // Initially unchecked.
            doc.Form.Add(checkBox, 1);

            // ------------------------------------------------------------
            // 2. Add a text box that will serve as the collapsible content.
            // ------------------------------------------------------------
            TextBoxField collapsible = new TextBoxField(page,
                new Aspose.Pdf.Rectangle(100, 600, 400, 730));
            collapsible.PartialName = "CollapsibleSection";
            collapsible.Value = "This is the hidden content that becomes visible when the checkbox is checked.";
            // Initially hide the field by setting the widget annotation flag to Hidden.
            collapsible.Flags = AnnotationFlags.Hidden;
            doc.Form.Add(collapsible, 1);

            // ------------------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility.
            // ------------------------------------------------------------
            string jsCode = @"
if (this.getField('ShowSection').value == 'Yes') {
    this.getField('CollapsibleSection').display = display.visible;
} else {
    this.getField('CollapsibleSection').display = display.hidden;
}";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            // Use the ReleaseMouseBtn event (mouse up) – the correct property on the Actions collection.
            checkBox.Actions.OnReleaseMouseBtn = jsAction;

            // ------------------------------------------------------------
            // 4. Save the PDF.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with collapsible section saved to '{outputPath}'.");
    }
}
