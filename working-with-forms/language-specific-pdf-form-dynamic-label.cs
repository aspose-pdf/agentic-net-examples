using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "LanguageSpecificForm.pdf";

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Set the natural language for the document (e.g., English).
            doc.TaggedContent.SetLanguage("en-US");

            // -------------------------------------------------
            // Create a text field that will serve as the label.
            // -------------------------------------------------
            // Rectangle coordinates: lower-left X, lower-left Y, upper-right X, upper-right Y.
            Aspose.Pdf.Rectangle labelRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField labelField = new TextBoxField(page, labelRect)
            {
                PartialName = "LabelField",   // Internal field name.
                Value = "Label"               // Initial displayed text.
            };
            doc.Form.Add(labelField);

            // -------------------------------------------------
            // Create a combo box for locale selection.
            // -------------------------------------------------
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 560, 300, 590);
            ComboBoxField localeCombo = new ComboBoxField(page, comboRect)
            {
                PartialName = "LocaleCombo"
            };
            // Add language options.
            localeCombo.AddOption("English");
            localeCombo.AddOption("French");
            localeCombo.AddOption("Spanish");
            doc.Form.Add(localeCombo);

            // -------------------------------------------------
            // Attach JavaScript to change the label based on selection.
            // -------------------------------------------------
            // The script runs when the combo box value is validated.
            // It sets the value of the label field according to the selected language.
            string js = @"
                var lbl = this.getField('LabelField');
                if (event.value == 'English') {
                    lbl.value = 'Name';
                } else if (event.value == 'French') {
                    lbl.value = 'Nom';
                } else if (event.value == 'Spanish') {
                    lbl.value = 'Nombre';
                }
            ";
            localeCombo.Actions.OnValidate = new JavascriptAction(js);

            // Save the PDF document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form created: {outputPath}");
    }
}