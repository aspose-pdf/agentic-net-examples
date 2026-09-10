using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "LocalizedForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set the document language (default for all content)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US"); // default language

            // -------------------------------------------------
            // Create a read‑only text box that will serve as the label
            // -------------------------------------------------
            // Position: lower‑left (50, 750), upper‑right (150, 770)
            Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(50, 750, 150, 770);
            TextBoxField labelField = new TextBoxField(page, labelRect);
            labelField.PartialName = "Label";
            labelField.ReadOnly = true;
            labelField.Value = "Name:"; // default English label
            doc.Form.Add(labelField);

            // -------------------------------------------------
            // Create a combo box for language selection
            // -------------------------------------------------
            // Position: lower‑left (200, 750), upper‑right (300, 770)
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(200, 750, 300, 770);
            ComboBoxField localeCombo = new ComboBoxField(page, comboRect);
            localeCombo.PartialName = "Locale";
            localeCombo.AddOption("en-US"); // English
            localeCombo.AddOption("fr-FR"); // French
            localeCombo.Value = "en-US";    // default selection
            doc.Form.Add(localeCombo);

            // -------------------------------------------------
            // Add JavaScript to change the label based on selection
            // -------------------------------------------------
            // The script runs when the combo box value changes (using OnCalculate)
            string js = @"
var lbl = this.getField('Label');
var sel = this.getField('Locale').value;
if (sel == 'en-US') {
    lbl.value = 'Name:';
} else if (sel == 'fr-FR') {
    lbl.value = 'Nom:';
}
";
            JavascriptAction jsAction = new JavascriptAction(js);
            // Attach the script to the combo box's calculate event (fires on value change)
            localeCombo.Actions.OnCalculate = jsAction;

            // -------------------------------------------------
            // Save the PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with language‑specific label saved to '{outputPath}'.");
    }
}
