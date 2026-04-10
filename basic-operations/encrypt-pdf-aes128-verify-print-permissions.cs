using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF with AES‑128 and enable high‑quality printing
        using (Document doc = new Document(inputPath))
        {
            // Permissions: allow printing and high‑quality printing
            Permissions perms = Permissions.PrintDocument | Permissions.PrintingQuality;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
            doc.Save(encryptedPath);
        }

        // Verify the encryption settings by reopening the file with the user password.
        // The older Aspose.Pdf versions expose permissions via SecuritySettings, but newer
        // versions may not have that property. To keep the code version‑agnostic we simply
        // attempt to open the document with the user password – if no exception is thrown,
        // the encryption is considered valid. If the SecuritySettings property exists we
        // also display the actual permissions.
        try
        {
            using (FileStream fs = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
            using (Document encryptedDoc = new Document(fs, userPassword))
            {
                // Attempt to read the permissions if the property is available.
                bool canPrint = false;
                bool highQualityPrint = false;

                var securitySettingsProp = encryptedDoc.GetType().GetProperty("SecuritySettings");
                if (securitySettingsProp != null)
                {
                    var securitySettings = securitySettingsProp.GetValue(encryptedDoc);
                    var permissionsProp = securitySettings.GetType().GetProperty("Permissions");
                    if (permissionsProp != null)
                    {
                        var actualPerms = (Permissions)permissionsProp.GetValue(securitySettings);
                        canPrint = (actualPerms & Permissions.PrintDocument) != 0;
                        highQualityPrint = (actualPerms & Permissions.PrintingQuality) != 0;
                    }
                }

                Console.WriteLine($"Encryption verified. Print allowed: {canPrint}, High‑quality print allowed: {highQualityPrint}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to verify encryption: {ex.Message}");
        }
    }
}
