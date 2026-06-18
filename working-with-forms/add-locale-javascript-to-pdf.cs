using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Define a JavaScript dictionary with translations and a helper
            //    function that updates a form field named "label" according to
            //    the selected locale.
            // -----------------------------------------------------------------
            string jsCode = @"
var translations = {
    'en' : 'Hello, World!',
    'fr' : 'Bonjour le monde!',
    'es' : '¡Hola Mundo!'
};

function setLabel(locale) {
    var field = this.getField('label');
    if (field != null && translations[locale] != undefined) {
        field.value = translations[locale];
    }
}
";

            // Add the script to the document‑level JavaScript collection.
            // The key can be any identifier; here we use "LocaleHelper".
            doc.JavaScript["LocaleHelper"] = jsCode;

            // -----------------------------------------------------------------
            // 2. Optionally, set an OpenAction that sets a default locale
            //    when the PDF is opened (e.g., English).
            // -----------------------------------------------------------------
            doc.OpenAction = new JavascriptAction("setLabel('en');");

            // -----------------------------------------------------------------
            // 3. Demonstrate enumeration of existing document JavaScript entries.
            //    JavaScriptCollection does not implement IEnumerable, so we
            //    iterate via its Keys property.
            // -----------------------------------------------------------------
            if (doc.JavaScript != null)
            {
                Console.WriteLine("Existing document JavaScript entries:");
                foreach (string key in doc.JavaScript.Keys)
                {
                    string script = doc.JavaScript[key];
                    Console.WriteLine($"Key: {key}");
                    Console.WriteLine($"Script: {script}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with locale JavaScript saved to '{outputPdf}'.");
    }
}