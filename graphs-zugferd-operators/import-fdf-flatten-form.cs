using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a single text box form field.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will be placed.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 560);

            // Create a text box field.
            TextBoxField nameField = new TextBoxField(page, fieldRect);
            nameField.PartialName = "NameField";
            nameField.Value = "Default";

            // Add the field to the document's form.
            doc.Form.Add(nameField);

            // Save the sample PDF.
            doc.Save("input.pdf");
        }

        // Step 2: Prepare XFDF data that will populate the form field.
        string xfdfContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                             "<xfdf xmlns=\"http://ns.adobe.com/xfdf/\" xml:space=\"preserve\">\n" +
                             "  <fields>\n" +
                             "    <field name=\"NameField\"><value>John Doe</value></field>\n" +
                             "  </fields>\n" +
                             "</xfdf>";

        // Step 3: Load the PDF, import XFDF data, flatten the form, and save the result.
        using (Document doc = new Document("input.pdf"))
        {
            using (MemoryStream xfdfStream = new MemoryStream(Encoding.UTF8.GetBytes(xfdfContent)))
            {
                // Import field values from the XFDF stream.
                XfdfReader.ReadFields(xfdfStream, doc);
            }

            // Flatten the form to make the fields non‑editable.
            doc.Form.Flatten();

            // Save the final PDF.
            doc.Save("output.pdf");
        }
    }
}
