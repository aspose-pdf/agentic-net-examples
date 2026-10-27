using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // Required for Permissions enum

class Program
{
    static void Main()
    {
        const string outputPath = "protected_form.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the form field (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box form field
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SecureField",
                Value = "Enter text here"
            };

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Set document encryption:
            // - userPassword allows opening the document and filling forms
            // - ownerPassword restricts other operations
            // - Permissions include FillForm so the user can edit the field after authentication
            Permissions perms = Permissions.PrintDocument | Permissions.FillForm;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with password‑protected form saved to '{outputPath}'.");
    }
}
