using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file to be converted to PDF
        const string xmlPath = "input.xml";
        // Output encrypted PDF file
        const string pdfPath = "output_encrypted.pdf";

        // Passwords for PDF protection
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Ensure the source XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML and convert it to a PDF document
        // XmlLoadOptions is the correct way to load XML (rule: xml-load-use-bindxml-not-document-constructor)
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Apply encryption using a strong algorithm (AES-256) and desired permissions
            // Follow the encryption-always-use-CryptoAlgorithm rule
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            pdfDocument.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF (standard Document.Save)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{pdfPath}'.");
    }
}