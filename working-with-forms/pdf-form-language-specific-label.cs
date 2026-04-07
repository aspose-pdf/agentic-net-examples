using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set the document language (default)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");

            // ---------- Language selection field (ComboBox) ----------
            // Position and size of the dropdown
            Aspose.Pdf.Rectangle langRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);
            // Create the combo box field on the page (ChoiceField is abstract)
            ComboBoxField langField = new ComboBoxField(page, langRect)
            {
                PartialName = "LanguageSelect",
                // Optional visual appearance
                Color = Aspose.Pdf.Color.LightGray
            };
            // Add language options (display value). Export value defaults to the same string.
            langField.AddOption("en-US"); // English
            langField.AddOption("fr-FR"); // Français
            langField.AddOption("es-ES"); // Español
            // Set default selected option (first item)
            langField.Selected = 0;

            // ---------- Label field that will change text ----------
            Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(50, 700, 300, 720);
            TextBoxField labelField = new TextBoxField(page, labelRect)
            {
                PartialName = "DynamicLabel",
                // Initial text (English)
                Value = "Name:",
                Color = Aspose.Pdf.Color.LightGray
            };

            // ---------- JavaScript to update label based on language ----------
            // The script runs when the language field value changes (use OnCalculate which fires on value change)
            string js = @"
var lang = this.getField('LanguageSelect').value;
var label = this.getField('DynamicLabel');
if (lang == 'en-US') {
    label.value = 'Name:';
} else if (lang == 'fr-FR') {
    label.value = 'Nom:';
} else if (lang == 'es-ES') {
    label.value = 'Nombre:';
}
";
            // Attach the JavaScript action to the language field
            langField.Actions.OnCalculate = new JavascriptAction(js);

            // Add fields to the form (page number is 1‑based)
            doc.Form.Add(langField, 1);
            doc.Form.Add(labelField, 1);

            // Save the PDF
            doc.Save("LanguageSpecificForm.pdf");
        }

        Console.WriteLine("PDF form with language‑specific field created successfully.");
    }
}
