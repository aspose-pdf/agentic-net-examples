using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class MyCustomSecurityHandler : ICustomSecurityHandler
{
    // Simple properties required by the interface
    public string Filter => "CustomFilter";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => "CustomSubFilter";
    public int Version => 1;

    // Store encryption parameters for potential use
    private EncryptionParameters _parameters;

    // Called during encryption to initialize the handler with parameters
    public void Initialize(EncryptionParameters parameters)
    {
        _parameters = parameters;
        Console.WriteLine("[CustomSecurity] Initialize called.");
    }

    // Called to calculate the encryption key; here we just log and return an empty key
    public byte[] CalculateEncryptionKey(string userKey)
    {
        Console.WriteLine("[CustomSecurity] CalculateEncryptionKey called.");
        return new byte[0];
    }

    // Decrypt data; log and return data unchanged (must return byte[] per interface)
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("[CustomSecurity] Decrypt called.");
        // In a real implementation you would decrypt the specified slice.
        // For demo purposes we simply return the original array.
        return data;
    }

    // Encrypt data; log and return data unchanged (must return byte[] per interface)
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("[CustomSecurity] Encrypt called.");
        // In a real implementation you would encrypt the specified slice.
        // For demo purposes we simply return the original array.
        return data;
    }

    // Encrypt the permissions field; log and return empty array
    public byte[] EncryptPermissions(int permissions)
    {
        Console.WriteLine("[CustomSecurity] EncryptPermissions called.");
        return new byte[0];
    }

    // Generate owner key; log and return empty array
    public byte[] GetOwnerKey(string userPassword, string ownerPassword)
    {
        Console.WriteLine("[CustomSecurity] GetOwnerKey called.");
        return new byte[0];
    }

    // Generate user key; log and return empty array
    public byte[] GetUserKey(string userPassword)
    {
        Console.WriteLine("[CustomSecurity] GetUserKey called.");
        return new byte[0];
    }

    // Check if supplied password is the owner password; log and return false for demo
    public bool IsOwnerPassword(string password)
    {
        Console.WriteLine("[CustomSecurity] IsOwnerPassword called.");
        return false;
    }

    // Check if supplied password is the user password; log and return true for demo
    public bool IsUserPassword(string password)
    {
        Console.WriteLine("[CustomSecurity] IsUserPassword called.");
        return true;
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string encryptedPdf = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a custom security handler instance
        MyCustomSecurityHandler customHandler = new MyCustomSecurityHandler();

        // Encrypt the PDF using the custom handler
        using (Document doc = new Document(inputPdf))
        {
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, customHandler);
            doc.Save(encryptedPdf); // Save encrypted PDF
        }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPdf}'.");

        // Open the encrypted PDF with the same custom handler to trigger logging
        using (Document encryptedDoc = new Document(encryptedPdf, userPassword, customHandler))
        {
            // Access a page to force decryption
            var page = encryptedDoc.Pages[1];
            Console.WriteLine($"Page count after opening encrypted PDF: {encryptedDoc.Pages.Count}");
        }
    }
}
