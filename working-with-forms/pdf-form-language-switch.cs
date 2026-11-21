using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "LanguageForm.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Language selection dropdown (ComboBox)
            var langRect = new Rectangle(50, 750, 200, 720);
            var langField = new ComboBoxField(page, langRect)
            {
                PartialName = "Language",
                Value = "English"
            };
            // Correct way to add items to a ComboBoxField
            langField.AddOption("English");
            langField.AddOption("Spanish");
            langField.AddOption("French");

            // Read‑only label fields that will be changed via JavaScript
            var label1 = new TextBoxField(page, new Rectangle(50, 680, 200, 650))
            {
                PartialName = "Label1",
                Value = "Name:",
                ReadOnly = true
            };

            var label2 = new TextBoxField(page, new Rectangle(50, 610, 200, 580))
            {
                PartialName = "Label2",
                Value = "Address:",
                ReadOnly = true
            };

            // Input fields for user data
            var input1 = new TextBoxField(page, new Rectangle(210, 680, 400, 650))
            {
                PartialName = "InputName"
            };

            var input2 = new TextBoxField(page, new Rectangle(210, 610, 400, 580))
            {
                PartialName = "InputAddress"
            };

            // JavaScript that updates label texts based on the selected language
            string js = @"
var lang = this.getField('Language').value;
if (lang == 'English') {
    this.getField('Label1').value = 'Name:';
    this.getField('Label2').value = 'Address:';
} else if (lang == 'Spanish') {
    this.getField('Label1').value = 'Nombre:';
    this.getField('Label2').value = 'Dirección:';
} else if (lang == 'French') {
    this.getField('Label1').value = 'Nom:';
    this.getField('Label2').value = 'Adresse:';
}
";

            // Attach the JavaScript to the combo box's calculation action (fires on value change)
            langField.Actions.OnCalculate = new JavascriptAction(js);

            // Add fields to the document's form (page 1)
            doc.Form.Add(langField, 1);
            doc.Form.Add(label1, 1);
            doc.Form.Add(label2, 1);
            doc.Form.Add(input1, 1);
            doc.Form.Add(input2, 1);

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form saved to '{outputPath}'.");
    }
}
