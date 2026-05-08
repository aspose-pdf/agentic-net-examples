using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "LocaleAwarePdf.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add a text box field that will display the translated label
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
            TextBoxField txtField = new TextBoxField(page, fieldRect);
            txtField.PartialName = "labelField";          // field name used in JavaScript
            txtField.Value = "";                         // initial empty value
            doc.Form.Add(txtField);

            // JavaScript dictionary with translations and a helper function
            string jsCode = @"
var translations = {
    'en': 'Hello',
    'fr': 'Bonjour',
    'es': 'Hola',
    'de': 'Hallo'
};

function setLocale(loc) {
    var f = this.getField('labelField');
    if (f != null && translations[loc] != undefined) {
        f.value = translations[loc];
    }
}

// Set default locale when the document is opened
setLocale('en');
";

            // Add the JavaScript to the document-level JavaScript collection
            // The key can be any identifier; here we use "LocaleScripts"
            doc.JavaScript["LocaleScripts"] = jsCode;

            // Optionally, add a page open action that could change locale dynamically
            // For demonstration, we call setLocale('fr') when the page is opened
            // (Uncomment the following lines to enable)
            // string pageOpenJs = "setLocale('fr');";
            // page.Actions.OnOpen = new JavascriptAction(pageOpenJs);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with locale-aware label saved to '{outputPath}'.");
    }
}