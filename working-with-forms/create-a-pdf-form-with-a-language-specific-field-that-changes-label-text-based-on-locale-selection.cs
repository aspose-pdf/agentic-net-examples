using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "LocalizedForm.pdf";

        // Create a new PDF document and add a blank page.
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing).
            doc.Pages.Add();

            // Bind the document to a FormEditor for form manipulation.
            FormEditor formEditor = new FormEditor(doc);

            // -----------------------------------------------------------------
            // 1. Add a combo box for locale selection.
            // -----------------------------------------------------------------
            // Field name: "LocaleCombo"
            // Initial value: "en-US"
            // Page number: 1
            // Rectangle: lower‑left (50, 750), upper‑right (200, 770)
            formEditor.AddField(
                Aspose.Pdf.Facades.FieldType.ComboBox,
                "LocaleCombo",
                "en-US",
                1,
                50f, 750f, 200f, 770f);

            // Define the list of locale options.
            formEditor.Items = new string[] { "en-US", "fr-FR", "es-ES" };

            // -----------------------------------------------------------------
            // 2. Add a text field that will display the label.
            // -----------------------------------------------------------------
            // Field name: "LabelField"
            // Initial value: "Name" (default for en-US)
            // Page number: 1
            // Rectangle: lower‑left (250, 750), upper‑right (450, 770)
            formEditor.AddField(
                Aspose.Pdf.Facades.FieldType.Text,
                "LabelField",
                "Name",
                1,
                250f, 750f, 450f, 770f);

            // -----------------------------------------------------------------
            // 3. Attach JavaScript to the combo box to change the label text.
            // -----------------------------------------------------------------
            // The script runs when the combo box value changes.
            string js = @"
var locale = this.getField('LocaleCombo').value;
var label  = this.getField('LabelField');
switch (locale) {
    case 'en-US':
        label.value = 'Name';
        break;
    case 'fr-FR':
        label.value = 'Nom';
        break;
    case 'es-ES':
        label.value = 'Nombre';
        break;
}";
            // Add the script to the combo box field.
            formEditor.AddFieldScript("LocaleCombo", js);

            // Save the PDF with the form fields.
            doc.Save(outputPath);

            // Close the FormEditor (optional, as it does not implement IDisposable).
            formEditor.Close();
        }

        Console.WriteLine($"PDF form created: {outputPath}");
    }
}