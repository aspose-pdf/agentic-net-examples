using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF with form fields
        const string outputPdf = "encrypted_filled.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPdf))
            {
                // ----- Fill form fields -----
                // Example: set values for fields named "Name" and "Date"
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // Access fields by name and assign values using the Field.Value property
                    Field nameField = doc.Form["Name"] as Field;
                    if (nameField != null)
                        nameField.Value = "John Doe";

                    Field dateField = doc.Form["Date"] as Field;
                    if (dateField != null)
                        dateField.Value = DateTime.Today.ToShortDateString();
                }

                // ----- Encrypt the document -----
                // Define permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Use AES-256 encryption (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form filled and encrypted PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
