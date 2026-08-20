using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageForm.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // ---------- Page 1 ----------
            // Add first page (1‑based indexing)
            Page page1 = doc.Pages.Add();

            // Define rectangle for the text box field (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a TextBox field on page 1
            TextBoxField txtField = new TextBoxField(page1, rect1)
            {
                Name = "txtName",          // full field name
                PartialName = "Name",      // displayed name
                Value = "John Doe"         // default value
            };

            // Add the field to the form on page 1 (page number = 1)
            doc.Form.Add(txtField, 1);

            // ---------- Page 2 ----------
            Page page2 = doc.Pages.Add();

            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 600, 120, 620);

            // Create a CheckBox field on page 2
            CheckboxField chkField = new CheckboxField(page2, rect2)
            {
                Name = "chkAgree",
                PartialName = "Agree",
                Value = "Yes",
                Checked = true
            };

            // Add the field to the form on page 2
            doc.Form.Add(chkField, 2);

            // ---------- Page 3 ----------
            Page page3 = doc.Pages.Add();

            Aspose.Pdf.Rectangle rect3 = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a ComboBox (dropdown) field on page 3
            ComboBoxField comboField = new ComboBoxField(page3, rect3)
            {
                Name = "cmbCountry",
                PartialName = "Country"
            };

            // Populate the combo box options
            comboField.AddOption("USA");
            comboField.AddOption("Canada");
            comboField.AddOption("Mexico");

            // Set default selected value (must match one of the options)
            comboField.Value = "USA";

            // Add the field to the form on page 3
            doc.Form.Add(comboField, 3);

            // Save the multi‑page PDF with distinct form fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
