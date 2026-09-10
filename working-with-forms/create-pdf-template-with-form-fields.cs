using System;
using System.Drawing; // System.Drawing.Color is used for DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document (lifecycle rule)
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define rectangles for the placeholder fields – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle nameFieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            Aspose.Pdf.Rectangle emailFieldRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            Aspose.Pdf.Rectangle addressFieldRect = new Aspose.Pdf.Rectangle(100, 550, 500, 640);

            // Create a TextBoxField for "Name" placeholder (note: pass the page, not the document)
            TextBoxField nameField = new TextBoxField(page, nameFieldRect)
            {
                PartialName = "Name",
                Value = "" // empty value as placeholder
            };
            nameField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a TextBoxField for "Email" placeholder
            TextBoxField emailField = new TextBoxField(page, emailFieldRect)
            {
                PartialName = "Email",
                Value = ""
            };
            emailField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a TextBoxField for "Address" placeholder (multiline)
            TextBoxField addressField = new TextBoxField(page, addressFieldRect)
            {
                PartialName = "Address",
                Value = "",
                Multiline = true
            };
            addressField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the fields to the document's form
            doc.Form.Add(nameField);
            doc.Form.Add(emailField);
            doc.Form.Add(addressField);

            // Optionally, add visual labels using TextFragment
            TextFragment nameLabel = new TextFragment("Name:")
            {
                Position = new Position(100, 735)
            };
            page.Paragraphs.Add(nameLabel);

            TextFragment emailLabel = new TextFragment("Email:")
            {
                Position = new Position(100, 685)
            };
            page.Paragraphs.Add(emailLabel);

            TextFragment addressLabel = new TextFragment("Address:")
            {
                Position = new Position(100, 645)
            };
            page.Paragraphs.Add(addressLabel);

            // Save the template PDF (save rule)
            const string outputPath = "Template.pdf";
            doc.Save(outputPath);
        }
    }
}
