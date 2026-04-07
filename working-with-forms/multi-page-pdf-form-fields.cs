using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class MultiPageFormCreator
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "MultiPageForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // Add three blank pages (1‑based indexing)
            // -----------------------------------------------------------------
            doc.Pages.Add(); // Page 1
            doc.Pages.Add(); // Page 2
            doc.Pages.Add(); // Page 3

            // -----------------------------------------------------------------
            // Page 1 – TextBox field (e.g., Name)
            // -----------------------------------------------------------------
            Page page1 = doc.Pages[1];
            // Rectangle(left, bottom, width, height)
            Aspose.Pdf.Rectangle rectName = new Aspose.Pdf.Rectangle(100, 600, 200, 20);
            TextBoxField txtName = new TextBoxField(page1, rectName)
            {
                PartialName = "Name",
                Value = "John Doe"
            };
            // Add the field to the form (field already positioned on page1)
            doc.Form.Add(txtName);

            // -----------------------------------------------------------------
            // Page 2 – CheckBox field (e.g., Subscribe)
            // -----------------------------------------------------------------
            Page page2 = doc.Pages[2];
            Aspose.Pdf.Rectangle rectSubscribe = new Aspose.Pdf.Rectangle(100, 600, 20, 20);
            CheckboxField chkSubscribe = new CheckboxField(page2, rectSubscribe)
            {
                PartialName = "Subscribe",
                Checked = true
            };
            doc.Form.Add(chkSubscribe);

            // -----------------------------------------------------------------
            // Page 3 – ComboBox (dropdown) field (e.g., Country)
            // -----------------------------------------------------------------
            Page page3 = doc.Pages[3];
            Aspose.Pdf.Rectangle rectCountry = new Aspose.Pdf.Rectangle(100, 600, 200, 20);
            ComboBoxField cmbCountry = new ComboBoxField(page3, rectCountry)
            {
                PartialName = "Country"
            };
            // Populate the list of choices
            cmbCountry.AddOption("USA");
            cmbCountry.AddOption("Canada");
            cmbCountry.AddOption("UK");
            // Set default selected value
            cmbCountry.Value = "USA";
            doc.Form.Add(cmbCountry);

            // -----------------------------------------------------------------
            // Save the multi‑page PDF with distinct form fields per page
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {Path.GetFullPath(outputPath)}");
    }
}
