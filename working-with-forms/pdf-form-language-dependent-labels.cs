using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ---------- Language selection combo box ----------
            // Rectangle coordinates: llx, lly, urx, ury
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            ComboBoxField languageCombo = new ComboBoxField(page, comboRect)
            {
                PartialName = "Language",          // field name used in JavaScript
                AlternateName = "Select Language",   // tooltip
                CommitImmediately = true               // apply change immediately
            };
            // Add language options
            languageCombo.AddOption("English");
            languageCombo.AddOption("Spanish");
            languageCombo.Selected = 0; // default to first option (0‑based index)

            // Add the combo box to the form
            doc.Form.Add(languageCombo);

            // ---------- Label field that will change dynamically ----------
            Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(100, 650, 250, 680);
            TextBoxField labelField = new TextBoxField(page, labelRect)
            {
                PartialName = "LabelName",
                ReadOnly = true,
                Value = "Name:" // default English label
            };
            doc.Form.Add(labelField);

            // ---------- JavaScript to update the label based on language ----------
            string js = @"
var lbl = this.getField('LabelName');
if (event.value == 'English') {
    lbl.value = 'Name:';
} else if (event.value == 'Spanish') {
    lbl.value = 'Nombre:';
}";
            // Attach the script to the combo box's calculate action (fires on value change)
            languageCombo.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF with the interactive form
            doc.Save("LanguageForm.pdf");
        }

        Console.WriteLine("PDF form with language selector created successfully.");
    }
}