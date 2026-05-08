using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTabOrder.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Enable manual tab ordering for the page (enum assignment)
            page.TabOrder = TabOrder.Manual;

            // Define three text box fields with distinct positions
            TextBoxField field1 = new TextBoxField(page,
                new Rectangle(100, 700, 300, 730))
            {
                PartialName = "FirstName",
                Color = Color.LightGray
            };

            TextBoxField field2 = new TextBoxField(page,
                new Rectangle(100, 650, 300, 680))
            {
                PartialName = "LastName",
                Color = Color.LightGray
            };

            TextBoxField field3 = new TextBoxField(page,
                new Rectangle(100, 600, 300, 630))
            {
                PartialName = "Email",
                Color = Color.LightGray
            };

            // Add fields to the document's form
            doc.Form.Add(field1);
            doc.Form.Add(field2);
            doc.Form.Add(field3);

            // Define a custom tab order: Email -> FirstName -> LastName
            var tabList = page.FieldsInTabOrder;
            tabList.Clear();               // remove default order
            tabList.Add(field3);           // Email first
            tabList.Add(field1);           // FirstName second
            tabList.Add(field2);           // LastName third

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields and custom tab order saved to '{outputPath}'.");
    }
}
