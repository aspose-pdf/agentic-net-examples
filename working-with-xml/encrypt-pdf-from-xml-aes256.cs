using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file and output PDF file paths
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document using XmlLoadOptions (no XSL required)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Define the permissions you want to allow in the encrypted PDF
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            // Apply encryption with the preferred AES‑256 algorithm
            pdfDocument.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF (no SaveOptions needed for PDF output)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Encrypted PDF created at '{pdfPath}'.");
    }
}