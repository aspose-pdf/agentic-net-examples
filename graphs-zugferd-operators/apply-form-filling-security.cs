using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace ApplyFormFillingSecurityExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with a single text box form field
            using (Document createDoc = new Document())
            {
                Page page = createDoc.Pages.Add();
                // Define the rectangle for the form field (left, bottom, right, top)
                Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
                // The TextBoxField constructor expects a Page and a Rectangle
                TextBoxField textBox = new TextBoxField(page, fieldRect);
                // Set the field name
                textBox.PartialName = "SampleField";
                // Add the field to the first page of the form
                createDoc.Form.Add(textBox, 1);
                // Save the temporary PDF
                createDoc.Save("input.pdf");
            }

            // Open the PDF and apply security settings that allow only form filling
            using (Document secureDoc = new Document("input.pdf"))
            {
                // Allow only form filling; all other permissions are denied
                Permissions allowedPermissions = Permissions.FillForm;
                // Encrypt the document with user and owner passwords using AES‑128 encryption
                secureDoc.Encrypt("userPassword", "ownerPassword", allowedPermissions, CryptoAlgorithm.AESx128);
                // Save the secured PDF
                secureDoc.Save("output.pdf");
            }
        }
    }
}
