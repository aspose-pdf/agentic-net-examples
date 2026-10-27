using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "LocalizedLabel.pdf";

        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a combo box for selecting the locale (en, fr, es)
            ComboBoxField localeField = new ComboBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 200, 730));
            localeField.PartialName = "locale";
            // Populate the combo box using AddOption (Option class has no public ctor)
            localeField.AddOption("en");
            localeField.AddOption("fr");
            localeField.AddOption("es");
            localeField.Value = "en"; // default selection
            doc.Form.Add(localeField);

            // Create a read‑only text field that will display the translated label
            TextBoxField labelField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 300, 680));
            labelField.PartialName = "myLabel";
            labelField.ReadOnly = true;
            doc.Form.Add(labelField);

            // JavaScript dictionary and helper function to update the label based on the selected locale
            string js = @"
var translations = {
    'en': 'Hello',
    'fr': 'Bonjour',
    'es': 'Hola'
};

function updateLabel() {
    var locale = this.getField('locale').value;
    var text = translations[locale] || '';
    this.getField('myLabel').value = text;
}

// Execute on document open
updateLabel();

// Attach the function to the combo box change event
this.getField('locale').setAction('MouseUp', 'updateLabel();');
";

            // Assign the script to the document's OpenAction so it runs when the PDF is opened
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF; Document.Save(string) writes a PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
