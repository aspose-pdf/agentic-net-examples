using System;
using System.IO;
using Aspose.Pdf;

class LoggingSecurityHandler : Aspose.Pdf.Security.ICustomSecurityHandler
{
    private Aspose.Pdf.Security.EncryptionParameters _parameters;

    // Required properties – simple static values for demonstration
    public string Filter => "CustomFilter";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => "CustomSubFilter";
    public int Version => 2;

    // Called when the PDF engine prepares encryption/decryption
    public void Initialize(Aspose.Pdf.Security.EncryptionParameters parameters)
    {
        _parameters = parameters;
        Console.WriteLine("CustomSecurityHandler initialized.");
    }

    // User‑key generation – log the password and return a placeholder
    public byte[] GetUserKey(string userPassword)
    {
        Console.WriteLine($"GetUserKey called with password: '{userPassword}'");
        return new byte[0];
    }

    // Owner‑key generation – log both passwords and return a placeholder
    public byte[] GetOwnerKey(string userPassword, string ownerPassword)
    {
        Console.WriteLine($"GetOwnerKey called with user: '{userPassword}', owner: '{ownerPassword}'");
        return new byte[0];
    }

    // Encryption key calculation – log and return a placeholder
    public byte[] CalculateEncryptionKey(string userPassword)
    {
        Console.WriteLine($"CalculateEncryptionKey called with password: '{userPassword}'");
        return new byte[0];
    }

    // Encryption of data – log and return the (unchanged) data as required by the interface
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("Encrypt called.");
        // Placeholder implementation – no real encryption performed.
        // Returning the original data satisfies the interface contract.
        return data;
    }

    // Decryption of data – log and return the (unchanged) data as required by the interface
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("Decrypt called.");
        // Placeholder implementation – no real decryption performed.
        return data;
    }

    // Permissions encryption – log and return a placeholder
    public byte[] EncryptPermissions(int permissions)
    {
        Console.WriteLine($"EncryptPermissions called with permissions: {permissions}");
        return new byte[0];
    }

    // Password checks – log each attempt and validate against known passwords
    public bool IsUserPassword(string password)
    {
        Console.WriteLine($"IsUserPassword check: '{password}'");
        return password == "user123";
    }

    public bool IsOwnerPassword(string password)
    {
        Console.WriteLine($"IsOwnerPassword check: '{password}'");
        return password == "owner123";
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted_custom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Instantiate the custom handler (will log all interactions)
        LoggingSecurityHandler customHandler = new LoggingSecurityHandler();

        // ---------- Encrypt the PDF using the custom handler ----------
        using (Document doc = new Document(inputPath))
        {
            // Example permission set – allow printing only
            Permissions perms = Permissions.PrintDocument;

            // Encrypt with user/owner passwords and the custom handler
            doc.Encrypt("user123", "owner123", perms, customHandler);

            // Save the encrypted document
            doc.Save(encryptedPath);
        }

        Console.WriteLine("PDF encrypted with custom security handler.");

        // ---------- Open with user password (triggers logging) ----------
        using (Document userDoc = new Document(encryptedPath, "user123", customHandler))
        {
            Console.WriteLine("Opened encrypted PDF with user password.");
            userDoc.Save("opened_user.pdf");
        }

        // ---------- Open with owner password (triggers logging) ----------
        using (Document ownerDoc = new Document(encryptedPath, "owner123", customHandler))
        {
            Console.WriteLine("Opened encrypted PDF with owner password.");
            ownerDoc.Save("opened_owner.pdf");
        }
    }
}
