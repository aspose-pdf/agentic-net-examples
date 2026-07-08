using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

            // Define a rectangle for the text field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box field that will display the translated label
            TextBoxField labelField = new TextBoxField(page, fieldRect)
            {
                PartialName = "myLabel",
                Value = "" // initial empty value; will be set by JavaScript
            };
            doc.Form.Add(labelField);

            // JavaScript dictionary of translations (key = locale, value = label text)
            string translationScript = @"
var translations = {
    'en': 'Hello',
    'fr': 'Bonjour',
    'es': 'Hola',
    'de': 'Hallo'
};

function setLabel(lang) {
    var field = this.getField('myLabel');
    if (field != null) {
        var txt = translations[lang];
        if (txt == null) txt = '';
        field.value = txt;
    }
}
";

            // Add the dictionary and helper function to the document's JavaScript collection
            doc.JavaScript["Translations"] = translationScript;

            // Document-level script that runs when the PDF is opened.
            // It reads the viewer's language (if available) and applies the appropriate label.
            string docOpenScript = @"
if (event && event.target) {
    // Attempt to get the viewer's language; fallback to 'en' if unavailable
    var userLang = (event.target.language) ? event.target.language : 'en';
    setLabel(userLang);
}
";
            doc.JavaScript["DocOpen"] = docOpenScript;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with localized label saved to '{outputPath}'.");
    }
}