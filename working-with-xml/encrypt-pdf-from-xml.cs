using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML and convert to PDF using XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Define permissions (allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the PDF with AES-256 encryption
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}