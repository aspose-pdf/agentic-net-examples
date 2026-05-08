using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);
            Aspose.Pdf.Rectangle label1Rect = new Aspose.Pdf.Rectangle(50, 700, 150, 720);
            Aspose.Pdf.Rectangle label2Rect = new Aspose.Pdf.Rectangle(50, 650, 150, 670);
            Aspose.Pdf.Rectangle input1Rect = new Aspose.Pdf.Rectangle(160, 700, 350, 720);
            Aspose.Pdf.Rectangle input2Rect = new Aspose.Pdf.Rectangle(160, 650, 350, 670);

            // Create a ComboBox for language selection
            ComboBoxField languageCombo = new ComboBoxField(page, comboRect);
            languageCombo.Name = "languageCombo";
            languageCombo.PartialName = "languageCombo";
            languageCombo.CommitImmediately = true; // trigger JavaScript on change
            languageCombo.AddOption("English");
            languageCombo.AddOption("French");
            languageCombo.AddOption("Spanish");
            // Add the combo box to the form
            doc.Form.Add(languageCombo);

            // Create label fields (as TextBoxFields) that will be updated via JavaScript
            TextBoxField label1 = new TextBoxField(page, label1Rect);
            label1.Name = "label1";
            label1.PartialName = "label1";
            label1.ReadOnly = true; // labels should not be editable
            doc.Form.Add(label1);

            TextBoxField label2 = new TextBoxField(page, label2Rect);
            label2.Name = "label2";
            label2.PartialName = "label2";
            label2.ReadOnly = true;
            doc.Form.Add(label2);

            // Create input fields for user data
            TextBoxField input1 = new TextBoxField(page, input1Rect);
            input1.Name = "input1";
            input1.PartialName = "input1";
            doc.Form.Add(input1);

            TextBoxField input2 = new TextBoxField(page, input2Rect);
            input2.Name = "input2";
            input2.PartialName = "input2";
            doc.Form.Add(input2);

            // JavaScript to change label texts based on selected language
            string js = @"
var lang = this.getSelectedItem();
if (lang == 'English') {
    this.getField('label1').value = 'Name:';
    this.getField('label2').value = 'Address:';
} else if (lang == 'French') {
    this.getField('label1').value = 'Nom:';
    this.getField('label2').value = 'Adresse:';
} else if (lang == 'Spanish') {
    this.getField('label1').value = 'Nombre:';
    this.getField('label2').value = 'Dirección:';
}
";
            // Attach JavaScript action to the combo box using a valid action property
            languageCombo.Actions.OnCalculate = new JavascriptAction(js);

            // Set default appearance for label fields (font and size)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            label1.DefaultAppearance = appearance;
            label2.DefaultAppearance = appearance;

            // Save the PDF
            doc.Save("LanguageForm.pdf");
        }

        Console.WriteLine("PDF form with dynamic language selection created successfully.");
    }
}
