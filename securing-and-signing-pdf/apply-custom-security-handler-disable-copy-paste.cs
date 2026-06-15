using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

// Custom security handler that disables copy‑paste (no ExtractContent permission)
class NoCopyPasteSecurityHandler : ICustomSecurityHandler
{
    // Required properties – simple values are sufficient for this example
    public string Filter => "Standard";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => null;
    public int Version => 2;

    // Minimal implementations – this handler does not perform real encryption
    public byte[] CalculateEncryptionKey(string userPassword)
    {
        // Derive a simple key from the password (not secure, just for demo)
        return System.Text.Encoding.UTF8.GetBytes(userPassword);
    }

    // NOTE: ICustomSecurityHandler expects a *return* value of byte[] for both Encrypt and Decrypt.
    // The stub implementation simply returns the original data unchanged.
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        // No decryption needed – data is stored unencrypted
        // Return the portion of the array that would be "decrypted" (for demo we return the whole array).
        return data;
    }

    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        // No encryption needed – data is stored unencrypted
        // Return the original data unchanged.
        return data;
    }

    public byte[] EncryptPermissions(int permissions)
    {
        // Return the permissions as a 4‑byte little‑endian array
        return BitConverter.GetBytes(permissions);
    }

    public byte[] GetOwnerKey(string ownerPassword, string userPassword)
    {
        // Simple concatenation (placeholder implementation)
        return System.Text.Encoding.UTF8.GetBytes(ownerPassword + userPassword);
    }

    public byte[] GetUserKey(string userPassword)
    {
        return System.Text.Encoding.UTF8.GetBytes(userPassword);
    }

    public void Initialize(EncryptionParameters parameters)
    {
        // No special initialization required for this stub
    }

    public bool IsOwnerPassword(string password)
    {
        // Not used in this simple scenario
        return false;
    }

    public bool IsUserPassword(string password)
    {
        // Not used in this simple scenario
        return false;
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply the custom security handler, and save
        using (Document doc = new Document(inputPath))
        {
            // Permissions without ExtractContent (copy‑paste) permission
            Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent;

            // Instantiate the custom handler
            var customHandler = new NoCopyPasteSecurityHandler();

            // Encrypt using the custom handler
            doc.Encrypt(userPassword, ownerPassword, perms, customHandler);

            // Save the protected PDF
            doc.Save(outputPath);
        }
    }
}
