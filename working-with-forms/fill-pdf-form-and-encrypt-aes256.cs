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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Fill form fields (example fields)
            Form form = doc.Form;

            // Cast the returned WidgetAnnotation to Field before accessing Value
            Field nameField = form["Name"] as Field;
            if (nameField != null)
                nameField.Value = "John Doe";

            Field dateField = form["Date"] as Field;
            if (dateField != null)
                dateField.Value = DateTime.Now.ToShortDateString();

            // Define permissions (allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using AES-256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form filled and encrypted PDF saved to '{outputPath}'.");
    }
}
