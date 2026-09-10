using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTabOrder.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Enable custom tab order for the page
            page.TabOrder = TabOrder.Manual;

            // Define field rectangles (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectFirstName = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            Aspose.Pdf.Rectangle rectLastName  = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            Aspose.Pdf.Rectangle rectAge       = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box for "First Name"
            TextBoxField firstNameField = new TextBoxField(page, rectFirstName)
            {
                PartialName = "FirstName",
                Color = Aspose.Pdf.Color.LightGray
            };
            // Add the field to the form on page 1 **before** setting TabOrder
            doc.Form.Add(firstNameField, 1);
            firstNameField.TabOrder = 1; // custom order index

            // Create a text box for "Last Name"
            TextBoxField lastNameField = new TextBoxField(page, rectLastName)
            {
                PartialName = "LastName",
                Color = Aspose.Pdf.Color.LightGray
            };
            doc.Form.Add(lastNameField, 1);
            lastNameField.TabOrder = 2; // custom order index

            // Create a number field for "Age"
            NumberField ageField = new NumberField(page, rectAge)
            {
                PartialName = "Age",
                Color = Aspose.Pdf.Color.LightGray
            };
            doc.Form.Add(ageField, 1);
            ageField.TabOrder = 3; // custom order index

            // Save the PDF (PDF format is default when no SaveOptions are supplied)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields and custom tab order saved to '{outputPath}'.");
    }
}
