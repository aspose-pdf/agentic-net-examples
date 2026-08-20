using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // for AnnotationFlags and JavascriptAction
using Aspose.Pdf.Forms;               // for form fields
using Aspose.Pdf.Drawing;             // for Rectangle (fully qualified later)

class Program
{
    static void Main()
    {
        const string outputPath = "ConditionalForm.pdf";

        // Create a new PDF document and a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ---------- Checkbox that controls the conditional section ----------
            // Position: lower‑left (100,700), upper‑right (120,720)
            Aspose.Pdf.Rectangle cbRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            CheckboxField showSectionCb = new CheckboxField(page, cbRect)
            {
                Name = "ShowSection",               // full field name
                PartialName = "ShowSection",
                ExportValue = "Yes",                // value when checked
                Checked = false                     // start unchecked
            };

            // JavaScript to show/hide the dependent field
            string jsCode = @"
if (this.getField('ShowSection').value == 'Yes') {
    this.getField('ConditionalText').display = display.visible;
} else {
    this.getField('ConditionalText').display = display.hidden;
}";
            showSectionCb.OnActivated = new JavascriptAction(jsCode);

            // Add the checkbox to the form
            doc.Form.Add(showSectionCb);

            // ---------- Conditional text field (initially hidden) ----------
            // Position: lower‑left (100,650), upper‑right (300,680)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField conditionalTxt = new TextBoxField(page, txtRect)
            {
                Name = "ConditionalText",
                PartialName = "ConditionalText",
                Contents = "This text appears only when the checkbox is checked."
            };

            // Hide the field initially
            conditionalTxt.Flags = AnnotationFlags.Hidden;

            // Add the text field to the form
            doc.Form.Add(conditionalTxt);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with conditional section saved to '{outputPath}'.");
    }
}