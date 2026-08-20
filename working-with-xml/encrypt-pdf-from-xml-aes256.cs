using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file and output PDF file paths
        const string xmlPath = "input.xml";
        const string pdfPath = "encrypted_output.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        // Load the XML document using XmlLoadOptions (no XSL required)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Open the document within a using block for deterministic disposal
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Define desired permissions (e.g., allow printing and content extraction)
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply encryption with a strong algorithm (AES‑256)
            doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Encrypted PDF successfully saved to '{pdfPath}'.");
    }
}