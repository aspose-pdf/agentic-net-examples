using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted_filled.pdf";
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
            // Fill form fields (example: a text field named "Name")
            Field nameField = doc.Form["Name"] as Field; // cast WidgetAnnotation to Field
            if (nameField != null)
            {
                nameField.Value = "John Doe";
            }

            // Add additional field assignments here as needed
            // e.g., fill a "Date" field
            Field dateField = doc.Form["Date"] as Field; // cast WidgetAnnotation to Field
            if (dateField != null)
            {
                dateField.Value = DateTime.Today.ToShortDateString();
            }

            // Define permissions and encrypt the document
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent | Permissions.FillForm;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF has been filled and encrypted: '{outputPath}'.");
    }
}