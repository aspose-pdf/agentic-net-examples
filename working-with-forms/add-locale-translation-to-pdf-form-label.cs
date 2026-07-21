using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Border class

class Program
{
    static void Main()
    {
        const string outputPath = "translated.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a read‑only label field (default text will be replaced by JavaScript)
            TextBoxField label = new TextBoxField(page, new Aspose.Pdf.Rectangle(50, 750, 150, 770))
            {
                PartialName = "NameLabel",
                Value = "Name",          // initial placeholder
                ReadOnly = true
            };
            // Border requires a parent annotation (see border-requires-parent-annotation rule)
            label.Border = new Border(label) { Width = 0 };
            // Add the field to the document's form collection (widgets cannot be added directly to page.Annotations)
            doc.Form.Add(label);

            // Create an editable input field
            TextBoxField input = new TextBoxField(page, new Aspose.Pdf.Rectangle(160, 750, 360, 770))
            {
                PartialName = "NameField",
                Value = ""
            };
            doc.Form.Add(input);

            // JavaScript dictionary of translations and a helper function
            string localeScript = @"
var translations = {
    'en': 'Name',
    'fr': 'Nom',
    'es': 'Nombre'
};

function setLocale(lang) {
    var lbl = this.getField('NameLabel');
    if (lbl) {
        lbl.value = translations[lang] || translations['en'];
    }
}
";

            // Add the script to the document‑level JavaScript collection (JavaScriptCollection class)
            doc.JavaScript["LocaleScript"] = localeScript;

            // Execute the function on document open to set the default language (e.g., English)
            doc.JavaScript["DocOpen"] = "setLocale('en');";

            // Save the PDF (document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
