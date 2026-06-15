using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("sample.pdf");
        }

        // Open the sample PDF and add AcroForm fields with a custom tab order
        using (Document doc = new Document("sample.pdf"))
        {
            // Set manual tab order on the first page
            doc.Pages[1].TabOrder = TabOrder.Manual;

            // Create a text box for Name
            TextBoxField nameField = new TextBoxField(doc.Pages[1], new Rectangle(100, 700, 300, 720));
            nameField.PartialName = "Name";
            nameField.Value = "John Doe";

            // Create a text box for Email
            TextBoxField emailField = new TextBoxField(doc.Pages[1], new Rectangle(100, 650, 300, 670));
            emailField.PartialName = "Email";
            emailField.Value = "john@example.com";

            // Create a text box for Phone
            TextBoxField phoneField = new TextBoxField(doc.Pages[1], new Rectangle(100, 600, 300, 620));
            phoneField.PartialName = "Phone";
            phoneField.Value = "123-456-7890";

            // Add fields to the form in the desired tab order (Name → Email → Phone)
            doc.Form.Add(nameField);
            doc.Form.Add(emailField);
            doc.Form.Add(phoneField);

            // Save the final PDF
            doc.Save("output.pdf");
        }
    }
}