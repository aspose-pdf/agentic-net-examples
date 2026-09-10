using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output encrypted PDF
        const string xmlPath      = "input.xml";
        const string encryptedPdf = "secure_output.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Permissions to allow printing and content extraction
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Verify input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Create an empty PDF document
            using (Document pdfDoc = new Document())
            {
                // Load XML content into the document (recommended approach)
                pdfDoc.BindXml(xmlPath);

                // Apply encryption using AES-256 algorithm
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                pdfDoc.Save(encryptedPdf);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}