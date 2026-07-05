using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Graph if needed (not used here)

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "LanguageForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // ---------- Create language selection ComboBox ----------
            // Position: left=100, bottom=700, right=250, top=730
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            ComboBoxField languageCombo = new ComboBoxField(page, comboRect)
            {
                PartialName = "langSelect",
                // Set default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
                // Commit the value immediately so JavaScript fires on change
                CommitImmediately = true
            };
            // Add language options
            languageCombo.AddOption("English");
            languageCombo.AddOption("Spanish");

            // ---------- Create label fields that will be updated ----------
            // Label 1 (e.g., "Name:" / "Nombre:")
            Aspose.Pdf.Rectangle label1Rect = new Aspose.Pdf.Rectangle(100, 650, 250, 670);
            TextBoxField label1 = new TextBoxField(page, label1Rect)
            {
                PartialName = "label1",
                ReadOnly = true,
                Value = "Name:",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Label 2 (e.g., "Address:" / "Dirección:")
            Aspose.Pdf.Rectangle label2Rect = new Aspose.Pdf.Rectangle(100, 620, 250, 640);
            TextBoxField label2 = new TextBoxField(page, label2Rect)
            {
                PartialName = "label2",
                ReadOnly = true,
                Value = "Address:",
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // ---------- Add fields to the form ----------
            // Page numbers are 1‑based
            doc.Form.Add(languageCombo, 1);
            doc.Form.Add(label1, 1);
            doc.Form.Add(label2, 1);

            // ---------- Attach JavaScript to the ComboBox ----------
            // This script updates the label fields based on the selected language
            string js = @"
var f1 = this.getField('label1');
var f2 = this.getField('label2');
if (event.value == 'English') {
    f1.value = 'Name:';
    f2.value = 'Address:';
} else if (event.value == 'Spanish') {
    f1.value = 'Nombre:';
    f2.value = 'Dirección:';
}";
            // Use OnCalculate (or OnModifyCharacter) which is a valid action for value change
            languageCombo.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with language dropdown saved to '{outputPath}'.");
    }
}
