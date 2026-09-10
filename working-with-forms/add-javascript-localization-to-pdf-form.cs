using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF or blank PDF
        const string outputPath = "localized.pdf";

        // Ensure the input PDF exists; if not, create a simple one
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                // Add a single page
                blank.Pages.Add();
                // Add a textbox field that will display the translated label
                TextBoxField labelField = new TextBoxField(blank.Pages[1], new Rectangle(100, 700, 300, 750))
                {
                    PartialName = "LabelField",
                    Value = "" // initial empty value
                };
                blank.Form.Add(labelField);
                blank.Save(inputPath);
            }
        }

        // Load the PDF, add JavaScript dictionary and locale handling
        using (Document doc = new Document(inputPath))
        {
            // 1. Add a JavaScript dictionary of translations
            // The dictionary maps locale codes to the text that should appear in the label field.
            string translationScript = @"
var translations = {
    'en': 'Hello',
    'fr': 'Bonjour',
    'es': 'Hola',
    'de': 'Hallo'
};

function setLocale(lang) {
    var txt = translations[lang] || '';
    // 'LabelField' is the name of the textbox field added earlier
    this.getField('LabelField').value = txt;
}
";

            // Add the script to the document's JavaScript collection
            // Use fully qualified type name to avoid ambiguity.
            Aspose.Pdf.JavaScriptCollection js = doc.JavaScript;
            js["Translations"] = translationScript;

            // 2. Add a document‑open action that sets a default locale (e.g., English)
            js["DocOpen"] = "setLocale('en');";

            // 3. (Optional) expose a function that can be called from external tools to change locale
            // This can be invoked via PDF viewers that support JavaScript.
            js["ChangeLocale"] = "function ChangeLocale(lang) { setLocale(lang); }";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript dictionary saved to '{outputPath}'.");
    }
}