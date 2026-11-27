using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string reportPath = "security_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Passwords used for encryption – keep them accessible for later inspection.
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Encrypt the PDF using AES‑256 and basic permissions
        try
        {
            using (Document doc = new Document(inputPath))
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(encryptedPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Encryption failed: {ex.Message}");
            return;
        }

        // Inspect security properties of the encrypted PDF
        try
        {
            // PdfFileInfo provides IsEncrypted flag.
            PdfFileInfo info = new PdfFileInfo(encryptedPath);

            // Permissions are not exposed via PdfFileInfo. Load the document with the owner password
            // and read the Permissions property from the Document object.
            Permissions encryptedPermissions;
            using (Document encryptedDoc = new Document(encryptedPath, ownerPassword))
            {
                // In older Aspose.Pdf versions Permissions may be an int; cast to the enum safely.
                encryptedPermissions = (Permissions)encryptedDoc.Permissions;
            }

            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("PDF Security Inspection Report");
                writer.WriteLine($"File: {encryptedPath}");
                writer.WriteLine($"Is Encrypted: {info.IsEncrypted}");
                writer.WriteLine($"Permissions: {encryptedPermissions}");
            }

            Console.WriteLine($"Report saved to {reportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Inspection failed: {ex.Message}");
        }
    }
}
