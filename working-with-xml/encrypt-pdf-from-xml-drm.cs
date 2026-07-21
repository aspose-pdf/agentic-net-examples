using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Security;      // Note: CryptoAlgorithm and Permissions are in Aspose.Pdf (no separate namespace needed, but including does not cause error)

class Program
{
    static void Main()
    {
        // Paths for input XML and output encrypted PDF
        const string xmlInputPath   = "input.xml";
        const string pdfOutputPath  = "secure_output.pdf";

        // Passwords for encryption
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify the XML file exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load XML and convert it to a PDF document using XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions(); // no XSL required
            using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
            {
                // Define permissions (e.g., allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply encryption using the recommended CryptoAlgorithm (AES-256)
                pdfDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}