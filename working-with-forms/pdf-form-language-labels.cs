using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "LanguageForm.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ---------- ComboBox for language selection ----------
            // Rectangle: left, bottom, width, height
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            ComboBoxField languageCombo = new ComboBoxField(page, comboRect);
            languageCombo.Name = "LanguageCombo";
            languageCombo.PartialName = "LanguageCombo";
            languageCombo.Border = new Border(languageCombo) { Width = 1 };
            languageCombo.Color = Aspose.Pdf.Color.LightGray;
            // Add language options
            languageCombo.AddOption("English");
            languageCombo.AddOption("Spanish");
            languageCombo.AddOption("French");

            // ---------- TextBox fields that will act as labels ----------
            // English label (default)
            Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(100, 650, 250, 670);
            TextBoxField labelField = new TextBoxField(page, labelRect);
            labelField.Name = "LabelField";
            labelField.PartialName = "LabelField";
            labelField.Value = "Name:";
            labelField.Border = new Border(labelField) { Width = 0 }; // invisible border
            labelField.Color = Aspose.Pdf.Color.Transparent;

            // Input field for the actual data
            Aspose.Pdf.Rectangle inputRect = new Aspose.Pdf.Rectangle(260, 650, 400, 670);
            TextBoxField inputField = new TextBoxField(page, inputRect);
            inputField.Name = "InputField";
            inputField.PartialName = "InputField";
            inputField.Border = new Border(inputField) { Width = 1 };
            inputField.Color = Aspose.Pdf.Color.LightGray;

            // ---------- JavaScript to change label based on selection ----------
            // The script runs when the combo box value changes.
            // It updates the value of the label field.
            string js = @"
                var selected = event.value;
                var label = this.getField('LabelField');
                if (selected == 'English') {
                    label.value = 'Name:';
                } else if (selected == 'Spanish') {
                    label.value = 'Nombre:';
                } else if (selected == 'French') {
                    label.value = 'Nom:';
                }
            ";
            // Use a valid action property – OnCalculate works for value‑change events.
            languageCombo.Actions.OnCalculate = new JavascriptAction(js);

            // Add fields to the document form
            doc.Form.Add(languageCombo);
            doc.Form.Add(labelField);
            doc.Form.Add(inputField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form saved to '{outputPath}'.");
    }
}
