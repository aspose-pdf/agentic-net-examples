using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a text box field that will display the translated label
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
            TextBoxField labelField = new TextBoxField(page, rect)
            {
                // Initial placeholder text
                Value = "Label",
                // Give the field a name so it can be accessed from JavaScript
                PartialName = "myLabel"
            };
            // Add the field to the document's form collection
            doc.Form.Add(labelField);

            // JavaScript dictionary of translations (language code -> text)
            // The script is stored in the PDF's JavaScript collection with a key "Translations"
            string jsTranslations = @"
var translations = {
    'en': 'Hello',
    'fr': 'Bonjour',
    'es': 'Hola',
    'de': 'Hallo'
};

function applyLabel(lang) {
    var field = this.getField('myLabel');
    if (field != null) {
        var txt = translations[lang];
        if (txt == null) { txt = ''; }
        field.value = txt;
    }
}
";
            // Add the script to the document
            doc.JavaScript["Translations"] = jsTranslations;

            // JavaScript that runs when the document is opened.
            // It detects the viewer's language (if available) and applies the appropriate label.
            // Fallback to English if detection fails.
            string jsOnOpen = @"
var userLang = '';
if (typeof app != 'undefined' && typeof app.language != 'undefined') {
    userLang = app.language;
}
if (userLang == '') { userLang = 'en'; }
applyLabel(userLang);
";
            // Store the open-action script under a distinct key
            doc.JavaScript["OnOpen"] = jsOnOpen;

            // Save the PDF to disk
            const string outputPath = "LocaleLabel.pdf";
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with locale‑aware label created successfully.");
    }
}