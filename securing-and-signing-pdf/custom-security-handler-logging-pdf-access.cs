using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Correct namespace for security interfaces

// Custom security handler that logs every security‑related call.
class LoggingSecurityHandler : ICustomSecurityHandler
{
    // Store parameters for possible future use
    private EncryptionParameters _parameters;

    // Simple properties required by the interface
    public string Filter => "CustomLogHandler";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => "CustomLogSubFilter";
    public int Version => 2;

    // Called during encryption/decryption initialization
    public void Initialize(EncryptionParameters parameters)
    {
        _parameters = parameters;
        Console.WriteLine("[Log] Initialize called.");
    }

    // Create user key (placeholder implementation)
    public byte[] GetUserKey(string userPassword)
    {
        Console.WriteLine($"[Log] GetUserKey called with password: '{userPassword}'");
        return new byte[0];
    }

    // Create owner key (placeholder implementation)
    public byte[] GetOwnerKey(string userPassword, string ownerPassword)
    {
        Console.WriteLine($"[Log] GetOwnerKey called with user: '{userPassword}', owner: '{ownerPassword}'");
        return new byte[0];
    }

    // Calculate encryption key (placeholder implementation)
    public byte[] CalculateEncryptionKey(string userPassword)
    {
        Console.WriteLine($"[Log] CalculateEncryptionKey called with password: '{userPassword}'");
        return new byte[0];
    }

    // Encrypt data (no actual encryption, just log)
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("[Log] Encrypt called.");
        // Return the original data slice unchanged
        byte[] result = new byte[count];
        Array.Copy(data, offset, result, 0, count);
        return result;
    }

    // Decrypt data (no actual decryption, just log)
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("[Log] Decrypt called.");
        // Return the original data slice unchanged
        byte[] result = new byte[count];
        Array.Copy(data, offset, result, 0, count);
        return result;
    }

    // Encrypt permissions field (placeholder)
    public byte[] EncryptPermissions(int permissions)
    {
        Console.WriteLine($"[Log] EncryptPermissions called with permissions: {permissions}");
        return new byte[0];
    }

    // Check if supplied password is the user password
    public bool IsUserPassword(string password)
    {
        Console.WriteLine($"[Log] IsUserPassword check for: '{password}'");
        // For demo purposes, accept any password
        return true;
    }

    // Check if supplied password is the owner password
    public bool IsOwnerPassword(string password)
    {
        Console.WriteLine($"[Log] IsOwnerPassword check for: '{password}'");
        // For demo purposes, accept any password
        return true;
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the custom handler instance
        LoggingSecurityHandler customHandler = new LoggingSecurityHandler();

        // ---------- Encrypt the PDF ----------
        using (Document doc = new Document(inputPath))
        {
            // Set desired permissions (example: allow printing and content extraction)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt using the custom security handler
            doc.Encrypt(userPassword, ownerPassword, perms, customHandler);

            // Save the encrypted document
            doc.Save(encryptedPath);
            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }

        // ---------- Open the encrypted PDF (this will trigger logging) ----------
        using (Document encryptedDoc = new Document(encryptedPath, userPassword, customHandler))
        {
            // Save a decrypted copy to verify access
            encryptedDoc.Save(decryptedPath);
            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
    }
}
