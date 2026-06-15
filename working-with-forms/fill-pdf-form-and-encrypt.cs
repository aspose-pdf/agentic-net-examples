using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF with form fields
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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Fill form fields safely – use concrete field types that expose the Value property
                if (doc.Form.HasField("Name"))
                {
                    if (doc.Form["Name"] is TextBoxField nameField)
                    {
                        nameField.Value = "John Doe";
                    }
                }

                if (doc.Form.HasField("Date"))
                {
                    if (doc.Form["Date"] is TextBoxField dateField)
                    {
                        dateField.Value = DateTime.Today.ToShortDateString();
                    }
                }

                // Apply encryption – use AES-256 and allow printing & content extraction
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form filled and encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
