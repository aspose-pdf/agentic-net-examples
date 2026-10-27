using System;
using System.IO;
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

            // ---------- Language selection dropdown ----------
            // Rectangle: lower-left X, lower-left Y, upper-right X, upper-right Y
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);
            ComboBoxField languageCombo = new ComboBoxField(page, comboRect);
            languageCombo.PartialName = "LanguageSelect";
            // Add options using AddOption (Option class has no public ctor)
            languageCombo.AddOption("English");
            languageCombo.AddOption("Spanish");
            languageCombo.Value = "English"; // default selection
            languageCombo.ReadOnly = false;
            // Add the combo box to the document's form
            doc.Form.Add(languageCombo);

            // ---------- Label fields (read‑only) ----------
            Aspose.Pdf.Rectangle label1Rect = new Aspose.Pdf.Rectangle(50, 700, 150, 720);
            TextBoxField labelName = new TextBoxField(page, label1Rect);
            labelName.PartialName = "LabelName";
            labelName.ReadOnly = true;
            labelName.Value = "Name:"; // default English label
            doc.Form.Add(labelName);

            Aspose.Pdf.Rectangle label2Rect = new Aspose.Pdf.Rectangle(50, 650, 150, 670);
            TextBoxField labelAddress = new TextBoxField(page, label2Rect);
            labelAddress.PartialName = "LabelAddress";
            labelAddress.ReadOnly = true;
            labelAddress.Value = "Address:"; // default English label
            doc.Form.Add(labelAddress);

            // ---------- Input fields ----------
            Aspose.Pdf.Rectangle nameFieldRect = new Aspose.Pdf.Rectangle(200, 700, 400, 720);
            TextBoxField nameField = new TextBoxField(page, nameFieldRect);
            nameField.PartialName = "Name";
            doc.Form.Add(nameField);

            Aspose.Pdf.Rectangle addressFieldRect = new Aspose.Pdf.Rectangle(200, 650, 400, 670);
            TextBoxField addressField = new TextBoxField(page, addressFieldRect);
            addressField.PartialName = "Address";
            doc.Form.Add(addressField);

            // ---------- JavaScript to change labels ----------
            // The script runs when the combo box value changes.
            // It updates the read‑only label fields based on the selected language.
            string js = @"
var lblName = this.getField('LabelName');
var lblAddr = this.getField('LabelAddress');
if (event.value == 'English') {
    lblName.value = 'Name:';
    lblAddr.value = 'Address:';
} else if (event.value == 'Spanish') {
    lblName.value = 'Nombre:';
    lblAddr.value = 'Dirección:';
}";
            // Use OnCalculate to trigger when the field value changes
            languageCombo.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with language dropdown saved to '{outputPath}'.");
    }
}
