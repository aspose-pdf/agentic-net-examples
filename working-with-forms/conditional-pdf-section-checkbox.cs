using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "ConditionalForm.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a checkbox that will control visibility
            // -------------------------------------------------
            // Rectangle: left, bottom, right, top
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField chk = new CheckboxField(page, chkRect)
            {
                Name = "ShowDetails",               // field name used in JavaScript
                PartialName = "ShowDetails",
                ExportValue = "Yes",                // value when checked
                Checked = false,                    // initially unchecked
                Color = Aspose.Pdf.Color.Blue
            };
            doc.Form.Add(chk);

            // -------------------------------------------------
            // 2. Create a text field that will be shown/hidden
            // -------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 700, 300, 730);
            TextBoxField txt = new TextBoxField(page, txtRect)
            {
                Name = "DetailsField",
                PartialName = "DetailsField",
                Value = "This text appears only when the box is checked.",
                ReadOnly = false,
                Color = Aspose.Pdf.Color.LightGray
            };
            doc.Form.Add(txt);

            // -------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility
            // -------------------------------------------------
            string js = @"
var f = this.getField('DetailsField');
if (event.target.value == 'Yes')
    f.display = display.visible;
else
    f.display = display.hidden;
";
            // Use the OnCalculate action (fires when the field value changes)
            chk.Actions.OnCalculate = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Hide the conditional field initially via a document‑level script
            // -------------------------------------------------
            // Document‑level JavaScript runs when the PDF is opened.
            doc.OpenAction = new JavascriptAction("this.getField('DetailsField').display = display.hidden;");

            // -------------------------------------------------
            // 5. Save the PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with conditional section saved to '{outputPath}'.");
    }
}
