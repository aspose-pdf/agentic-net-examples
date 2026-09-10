using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Added for ICustomSecurityHandler and EncryptionParameters

// Custom security handler that logs every password check and encryption step.
public class CustomSecurityHandler : ICustomSecurityHandler
{
    // Store passwords for simple validation.
    private string _userPassword;
    private string _ownerPassword;

    // ICustomSecurityHandler required properties.
    public string Filter => "CustomHandler";
    public int KeyLength => 128;          // Example key length.
    public int Revision => 4;             // Example revision.
    public string SubFilter => null;      // Not used.
    public int Version => 2;              // Example version.

    // Called when the handler is initialized for encryption/decryption.
    public void Initialize(EncryptionParameters parameters)
    {
        Console.WriteLine("CustomSecurityHandler: Initialize called.");
        // No special initialization needed for this example.
    }

    // Create a user key from the supplied password.
    public byte[] GetUserKey(string userPassword)
    {
        Console.WriteLine($"CustomSecurityHandler: GetUserKey called (password = '{userPassword}').");
        _userPassword = userPassword;
        return System.Text.Encoding.UTF8.GetBytes(userPassword);
    }

    // Create an owner key from the supplied passwords.
    public byte[] GetOwnerKey(string userPassword, string ownerPassword)
    {
        Console.WriteLine($"CustomSecurityHandler: GetOwnerKey called (user = '{userPassword}', owner = '{ownerPassword}').");
        _ownerPassword = ownerPassword;
        return System.Text.Encoding.UTF8.GetBytes(ownerPassword);
    }

    // Verify whether a password is the user password.
    public bool IsUserPassword(string password)
    {
        Console.WriteLine($"CustomSecurityHandler: IsUserPassword check for '{password}'.");
        return password == _userPassword;
    }

    // Verify whether a password is the owner password.
    public bool IsOwnerPassword(string password)
    {
        Console.WriteLine($"CustomSecurityHandler: IsOwnerPassword check for '{password}'.");
        return password == _ownerPassword;
    }

    // Calculate the encryption key (simple pass‑through in this demo).
    public byte[] CalculateEncryptionKey(string userPassword)
    {
        Console.WriteLine($"CustomSecurityHandler: CalculateEncryptionKey for '{userPassword}'.");
        return System.Text.Encoding.UTF8.GetBytes(userPassword);
    }

    // Encrypt a data block (no real encryption – just logs and returns the data unchanged).
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("CustomSecurityHandler: Encrypt called.");
        byte[] result = new byte[count];
        Array.Copy(data, offset, result, 0, count);
        return result;
    }

    // Decrypt a data block (no real decryption – just logs and returns the data unchanged).
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        Console.WriteLine("CustomSecurityHandler: Decrypt called.");
        byte[] result = new byte[count];
        Array.Copy(data, offset, result, 0, count);
        return result;
    }

    // Encrypt the permissions integer (simple conversion to bytes for demonstration).
    public byte[] EncryptPermissions(int permissions)
    {
        Console.WriteLine($"CustomSecurityHandler: EncryptPermissions called (permissions = {permissions}).");
        return BitConverter.GetBytes(permissions);
    }
}

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Ensure the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Instantiate the custom handler.
        CustomSecurityHandler customHandler = new CustomSecurityHandler();

        // ---------- Encrypt the PDF using the custom handler ----------
        using (Document doc = new Document(inputPath))
        {
            // Encrypt with desired permissions and the custom handler.
            doc.Encrypt(userPassword, ownerPassword, Permissions.PrintDocument, customHandler);
            doc.Save(encryptedPath); // Save the encrypted document.
        }

        Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");

        // ---------- Open the encrypted PDF – each access attempt will be logged ----------
        using (Document encryptedDoc = new Document(encryptedPath, userPassword, customHandler))
        {
            // Accessing the document (e.g., reading page count) triggers password checks.
            Console.WriteLine($"Encrypted PDF has {encryptedDoc.Pages.Count} page(s).");
        }
    }
}
