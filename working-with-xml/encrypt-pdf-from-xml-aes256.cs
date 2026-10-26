using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string encryptedPdfPath = "protected.pdf";
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the XML representation of a PDF document
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Set desired permissions (allow printing, disallow content extraction)
                Permissions permissions = Permissions.PrintDocument;

                // Apply encryption with a strong algorithm (AES 256‑bit)
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdfPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
