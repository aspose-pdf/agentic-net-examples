using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page where the form will be placed
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create the checkbox that will control the section
            // -------------------------------------------------
            // Position: lower‑left (50,750) – upper‑right (70,770)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField expandChk = new CheckboxField(page, chkRect)
            {
                Name = "ExpandSection",          // field name
                ExportValue = "Yes",             // value when checked
                Checked = false,                 // start unchecked
                // JavaScript that shows/hides the target field
                OnActivated = new JavascriptAction(
                    "var f = this.getField('SectionText'); " +
                    "if (event.target.value == 'Yes') { f.display = display.visible; } " +
                    "else { f.display = display.hidden; }")
            };
            // Add the checkbox to the document's form
            doc.Form.Add(expandChk);

            // -------------------------------------------------
            // 2. Create the collapsible text field (initially hidden)
            // -------------------------------------------------
            // Position: lower‑left (50,700) – upper‑right (300,740)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 700, 300, 740);
            TextBoxField collapsibleText = new TextBoxField(page, txtRect)
            {
                Name = "SectionText",            // field name referenced in JavaScript
                Multiline = true,
                Value = "This is the collapsible section content. " +
                        "It becomes visible when the checkbox above is checked.",
                // Hide the field initially
                Flags = AnnotationFlags.Hidden,
                // Optional: make the field read‑only so the user cannot edit it
                ReadOnly = true
            };
            doc.Form.Add(collapsibleText);

            // -------------------------------------------------
            // 3. Save the PDF
            // -------------------------------------------------
            doc.Save("CollapsibleForm.pdf");
        }

        Console.WriteLine("PDF with collapsible section created successfully.");
    }
}