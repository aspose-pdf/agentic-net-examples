using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "CollapsibleForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create the checkbox that will control the section
            // -------------------------------------------------
            // Position and size of the checkbox
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            // Create the checkbox field on the page
            CheckboxField chkBox = new CheckboxField(page, chkRect)
            {
                Name = "showSection",               // internal field name
                AlternateName = "Show Section",     // tooltip shown in Acrobat
                Checked = false,                    // initially unchecked
                ExportValue = "Yes"                 // value when checked
            };

            // JavaScript to toggle visibility of the target field
            string js = @"
var f = this.getField('section');
if (event.target.value == 'Yes')
{
    f.display = display.visible;
}
else
{
    f.display = display.hidden;
}";
            // Assign the JavaScript to the checkbox activation event
            chkBox.OnActivated = new JavascriptAction(js);

            // -------------------------------------------------
            // 2. Create the collapsible section (a simple text box)
            // -------------------------------------------------
            // Position and size of the collapsible content
            Aspose.Pdf.Rectangle secRect = new Aspose.Pdf.Rectangle(50, 650, 300, 730);
            TextBoxField sectionField = new TextBoxField(page, secRect)
            {
                Name = "section",                                   // internal field name referenced in JS
                Contents = "This is the collapsible content.\nIt becomes visible when the checkbox is checked.",
                // Hide the field initially
                Flags = AnnotationFlags.Hidden
            };

            // -------------------------------------------------
            // 3. Add fields to the document form
            // -------------------------------------------------
            doc.Form.Add(chkBox);
            doc.Form.Add(sectionField);

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with collapsible section saved to '{outputPath}'.");
    }
}