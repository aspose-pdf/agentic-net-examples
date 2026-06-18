using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------- Page 1 --------------------
            // Add first page
            Page page1 = doc.Pages.Add();

            // Define a rectangle for a text box field (left, bottom, width, height)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            // Create the text box field on page1
            TextBoxField txtField = new TextBoxField(page1, txtRect)
            {
                PartialName = "FirstName",
                Value = "John",
                // Optional visual settings
                Color = Aspose.Pdf.Color.LightGray,
                TextHorizontalAlignment = HorizontalAlignment.Center
            };
            // Add the field to the form on page 1 (page numbers are 1‑based)
            doc.Form.Add(txtField, 1);

            // -------------------- Page 2 --------------------
            // Add second page
            Page page2 = doc.Pages.Add();

            // Rectangle for a check box field
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);
            // Create the check box field on page2
            CheckboxField chkField = new CheckboxField(page2, chkRect)
            {
                PartialName = "Subscribe",
                Value = "Yes",               // Checked value
                // Optional visual settings
                Color = Aspose.Pdf.Color.LightGray
            };
            // Add the field to the form on page 2
            doc.Form.Add(chkField, 2);

            // -------------------- Page 3 --------------------
            // Add third page
            Page page3 = doc.Pages.Add();

            // Rectangle for a combo box (dropdown) field
            Aspose.Pdf.Rectangle choiceRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            // Create the combo box field on page3 (ComboBoxField derives from ChoiceField)
            ComboBoxField comboField = new ComboBoxField(page3, choiceRect)
            {
                PartialName = "Country",
                // Optional visual settings
                Color = Aspose.Pdf.Color.LightGray,
                // Set default selected value
                Value = "Canada"
            };
            // Add options to the combo box
            comboField.AddOption("USA");
            comboField.AddOption("Canada");
            comboField.AddOption("Mexico");
            // Add the field to the form on page 3
            doc.Form.Add(comboField, 3);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑page PDF with distinct AcroForm fields saved to '{outputPath}'.");
    }
}
