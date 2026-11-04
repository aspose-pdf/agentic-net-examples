using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Added for Permissions enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_owner_only.pdf";
        const string ownerPassword = "OwnerSecret123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF and encrypt it.
            // User password is empty (no password required to open), but permissions are restricted.
            // Owner password grants full rights.
            using (Document doc = new Document(inputPath))
            {
                // Define the permissions you want to allow for regular users.
                // Here we deny all permissions (full restriction) by using a zero value.
                Permissions userPermissions = (Permissions)0; // No permissions for users

                // Encrypt with owner password only.
                doc.Encrypt(
                    userPassword: "",               // No user password required to open
                    ownerPassword: ownerPassword,   // Owner password with full rights
                    permissions: userPermissions,   // Restricted permissions for users
                    cryptoAlgorithm: CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"PDF encrypted successfully. Output: {encryptedPath}");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
