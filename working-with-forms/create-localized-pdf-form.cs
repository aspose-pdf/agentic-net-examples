using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "localized_form.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a single page to host the form fields
            Page page = doc.Pages.Add();

            // Rectangle for the locale selector (ComboBox)
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(50, 750, 200, 720);
            ComboBoxField localeField = new ComboBoxField(page, comboRect);
            localeField.PartialName = "Locale";
            // Add options to the combo box
            localeField.AddOption("en-US"); // English
            localeField.AddOption("fr-FR"); // French
            localeField.Value = "en-US";    // Default selection

            // Rectangle for the label field (TextBox) whose text changes
            Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(250, 750, 450, 720);
            TextBoxField labelField = new TextBoxField(page, labelRect);
            labelField.PartialName = "Label";
            labelField.Value = "Name:"; // Default English label

            // Add fields to the document's form collection (page number is 1 because we have a single page)
            doc.Form.Add(localeField, 1);
            doc.Form.Add(labelField, 1);

            // JavaScript that updates the label based on the selected locale
            string js = @"
var locale = this.getField('Locale').value;
var label = this.getField('Label');
if (locale == 'en-US') {
    label.value = 'Name:';
} else if (locale == 'fr-FR') {
    label.value = 'Nom:';
}
";
            // Attach the script to the combo box's OnCalculate action (fires when the field value changes)
            localeField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF containing the interactive form
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form saved to '{outputPath}'.");
    }
}
