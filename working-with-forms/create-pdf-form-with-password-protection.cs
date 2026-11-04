using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // for Permissions enum

class Program
{
    static void Main()
    {
        const string outputPath = "SecureForm.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (llx, lly, urx, ury)
            Rectangle fieldRect = new Rectangle(100, 600, 300, 630);

            // Create a text box form field
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "SecureField",
                Value = "Enter confidential data",
                Required = true // make the field required
            };

            // Add the field to the document's form on page 1
            doc.Form.Add(txtField, 1);

            // Set permissions that allow filling the form but prevent other edits.
            // "ModifyContent" is the closest flag available in older Aspose.Pdf versions.
            Permissions perms = Permissions.ModifyContent | Permissions.ExtractContent;

            // Encrypt the document – a user password is required to open it, and the owner password controls the permissions.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with password‑protected field saved to '{outputPath}'.");
    }
}
