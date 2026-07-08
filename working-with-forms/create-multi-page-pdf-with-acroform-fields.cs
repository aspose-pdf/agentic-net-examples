using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // Page 1 – add a TextBox field
            // -------------------------------------------------
            // Add a blank page (1‑based indexing)
            Page page1 = doc.Pages.Add();

            // Define the rectangle for the field (left, bottom, width, height)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            // Create the TextBox field on the page
            TextBoxField txtField = new TextBoxField(page1, txtRect)
            {
                Name = "txtName",               // internal field name
                PartialName = "Name",           // displayed name (tooltip)
                Value = "Enter name here"
            };
            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // -------------------------------------------------
            // Page 2 – add a ComboBox (Choice) field
            // -------------------------------------------------
            Page page2 = doc.Pages.Add();

            Aspose.Pdf.Rectangle choiceRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            // Use ComboBoxField instead of abstract ChoiceField
            ComboBoxField comboField = new ComboBoxField(page2, choiceRect)
            {
                Name = "cmbCountry",
                PartialName = "Country",
                // Set the default selected value
                Value = "USA"
            };
            // Add options – Options property is read‑only, use AddOption()
            comboField.AddOption("USA");
            comboField.AddOption("Canada");
            comboField.AddOption("Mexico");
            doc.Form.Add(comboField, 2);

            // -------------------------------------------------
            // Page 3 – add a CheckBox field
            // -------------------------------------------------
            Page page3 = doc.Pages.Add();

            Aspose.Pdf.Rectangle checkRect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);
            CheckboxField checkField = new CheckboxField(page3, checkRect)
            {
                Name = "chkAgree",
                PartialName = "AgreeTerms",
                // Set the initial state (checked)
                Value = "Yes"
            };
            doc.Form.Add(checkField, 3);

            // -------------------------------------------------
            // Save the document (lifecycle rule: use Save)
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑page PDF with distinct AcroForm fields saved to '{outputPath}'.");
    }
}
