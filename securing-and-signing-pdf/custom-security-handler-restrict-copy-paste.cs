using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

// Custom security handler that performs no real encryption but allows us to set permissions.
class SimpleCustomSecurityHandler : Aspose.Pdf.Security.ICustomSecurityHandler
{
    private readonly string _userPassword;
    private readonly string _ownerPassword;

    public SimpleCustomSecurityHandler(string userPassword, string ownerPassword)
    {
        _userPassword = userPassword;
        _ownerPassword = ownerPassword;
    }

    // Required properties – provide simple constant values.
    public string Filter => "Standard";
    public int KeyLength => 256;
    public int Revision => 6;
    public string SubFilter => null;
    public int Version => 2;

    // Return a byte array derived from the supplied user key (no real calculation).
    public byte[] CalculateEncryptionKey(string userKey)
    {
        return System.Text.Encoding.UTF8.GetBytes(userKey ?? string.Empty);
    }

    // No actual encryption – just return the requested slice of the input data.
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        byte[] result = new byte[count];
        Array.Copy(data, offset, result, 0, count);
        return result;
    }

    // No actual decryption – just return the requested slice of the input data.
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        byte[] result = new byte[count];
        Array.Copy(data, offset, result, 0, count);
        return result;
    }

    // Simple representation of encrypted permissions (just the integer bytes).
    public byte[] EncryptPermissions(int permissions)
    {
        return BitConverter.GetBytes(permissions);
    }

    // Owner key generation – not used in this simple example.
    public byte[] GetOwnerKey(string ownerPassword, string userPassword)
    {
        return new byte[0];
    }

    // User key generation – not used in this simple example.
    public byte[] GetUserKey(string userPassword)
    {
        return new byte[0];
    }

    // Initialization hook – nothing required for this stub implementation.
    public void Initialize(Aspose.Pdf.Security.EncryptionParameters encryptionParameters)
    {
        // No initialization needed.
    }

    // Password checks against the stored passwords.
    public bool IsOwnerPassword(string password)
    {
        return string.Equals(password, _ownerPassword);
    }

    public bool IsUserPassword(string password)
    {
        return string.Equals(password, _userPassword);
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "restricted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Ensure the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF, apply custom security, and save.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Permissions: allow printing and content modification, but do NOT allow extraction (copy‑paste).
            Aspose.Pdf.Permissions permissions = Aspose.Pdf.Permissions.PrintDocument |
                                                Aspose.Pdf.Permissions.ModifyContent;

            // Create the custom handler with the desired passwords.
            var customHandler = new SimpleCustomSecurityHandler(userPassword, ownerPassword);

            // Encrypt using the custom handler.
            doc.Encrypt(userPassword, ownerPassword, permissions, customHandler);

            // Save the protected PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF encrypted with copy‑paste disabled. Saved to '{outputPath}'.");
    }
}