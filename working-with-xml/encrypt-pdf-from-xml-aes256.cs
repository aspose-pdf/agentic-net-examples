using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string xmlInput = "input.xml";
        const string encryptedPdf = "protected.pdf";
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        if (!File.Exists(xmlInput))
        {
            Console.Error.WriteLine($"XML input not found: {xmlInput}");
            return;
        }

        try
        {
            // Load the XML representation of a PDF document
            var loadOptions = new XmlLoadOptions();
            using (var doc = new Document(xmlInput, loadOptions))
            {
                // Define allowed permissions for the encrypted PDF
                Permissions perms = Permissions.PrintDocument |
                                    Permissions.ModifyTextAnnotations |
                                    Permissions.ExtractContent;

                // Apply encryption using AES‑256 (custom algorithm per security standards)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdf);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
