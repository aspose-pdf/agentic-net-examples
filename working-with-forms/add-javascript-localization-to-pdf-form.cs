using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input (none) and output PDF
        const string outputPath = "LocalizedLabel.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the form fields
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Create a hidden text field that will hold the current locale.
            //    This field can be changed programmatically or via a UI element.
            // -----------------------------------------------------------------
            TextBoxField localeField = new TextBoxField(page, new Rectangle(0, 0, 0, 0))
            {
                PartialName = "locale",
                Value = "en" // default locale
            };
            doc.Form.Add(localeField);

            // -----------------------------------------------------------------
            // 2. Create a visible label field that will display the translated text.
            // -----------------------------------------------------------------
            TextBoxField labelField = new TextBoxField(page, new Rectangle(100, 700, 400, 730))
            {
                PartialName = "lbl",
                // Make the field read‑only so the user cannot edit the label directly
                ReadOnly = true
            };
            // Remove border by setting width to 0 (BorderStyle.None does not exist)
            labelField.Border = new Border(labelField) { Width = 0 };
            doc.Form.Add(labelField);

            // -----------------------------------------------------------------
            // 3. Define a JavaScript dictionary with translations.
            //    The script will be executed whenever the locale field changes.
            // -----------------------------------------------------------------
            string js = @"
var translations = {
    'en': 'Hello, World!',
    'fr': 'Bonjour le monde!',
    'es': '¡Hola Mundo!',
    'de': 'Hallo Welt!'
};

// Function that updates the label based on the current locale
function updateLabel() {
    var loc = this.getField('locale').value;
    var txt = translations[loc];
    if (txt == null) { txt = translations['en']; } // fallback
    this.getField('lbl').value = txt;
}

// Initial call to set the label when the document is opened
updateLabel();
";

            // -----------------------------------------------------------------
            // 4. Attach the JavaScript to the document's OpenAction so it runs
            //    when the PDF is opened, and also to the locale field's
            //    OnValueChanged action so the label updates when the locale changes.
            // -----------------------------------------------------------------
            // OpenAction runs once when the PDF is opened
            doc.OpenAction = new JavascriptAction(js);

            // OnValueChanged runs each time the locale field value is modified
            // Use the OnCalculate action (valid for value change) to trigger the script
            localeField.Actions.OnCalculate = new JavascriptAction("updateLabel();");

            // -----------------------------------------------------------------
            // 5. Save the PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with localized label saved to '{outputPath}'.");
    }
}
