using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Input XML file to be converted to PDF
        const string xmlPath      = "input.xml";
        // Output encrypted PDF file
        const string encryptedPdf = "output_encrypted.pdf";

        // Passwords for PDF protection
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the source XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML with default options (no XSL transformation)
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            // Create PDF document from XML
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Define permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply password protection using AES-256 encryption
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