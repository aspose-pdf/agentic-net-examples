using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled_encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Fill form fields if the document contains a form
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // Example: set the "Name" field
                    Field nameField = doc.Form["Name"] as Field;
                    if (nameField != null)
                        nameField.Value = "John Doe";

                    // Example: set the "Date" field
                    Field dateField = doc.Form["Date"] as Field;
                    if (dateField != null)
                        dateField.Value = DateTime.Now.ToShortDateString();
                }

                // Define permissions for the encrypted PDF (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved and encrypted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
