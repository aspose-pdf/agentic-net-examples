using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class MyCustomSecurityHandler : ICustomSecurityHandler
{
    // Required read‑only properties – values can be arbitrary for this example
    public string Filter => "Standard";
    public int KeyLength => 128;
    public int Revision => 3;
    public string SubFilter => null;
    public int Version => 2;

    // Store the permissions that will be applied (no copy permission)
    private int _permissions;

    // Called during encryption to initialise the handler
    public void Initialize(EncryptionParameters parameters)
    {
        // Capture the permissions integer that Aspose passes (if any)
        _permissions = parameters.PermissionsInt;
    }

    // Return a user key – empty array is acceptable for a custom handler example
    public byte[] GetUserKey(string userPassword) => new byte[0];

    // Return an owner key – empty array is acceptable for a custom handler example
    public byte[] GetOwnerKey(string userPassword, string ownerPassword) => new byte[0];

    // Calculate the encryption key – empty array for this simple implementation
    public byte[] CalculateEncryptionKey(string userPassword) => new byte[0];

    // Encrypt a data block – pass through unchanged
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key) =>
        data;

    // Decrypt a data block – pass through unchanged
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key) =>
        data;

    // Encrypt the permissions field – return the integer as a 4‑byte array
    public byte[] EncryptPermissions(int permissions) =>
        BitConverter.GetBytes(permissions);

    // Password checks – always return false (no validation in this stub)
    public bool IsUserPassword(string password) => false;
    public bool IsOwnerPassword(string password) => false;
}

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Define permissions: allow printing but forbid copying (ExtractContent)
            Aspose.Pdf.Permissions permissions = Aspose.Pdf.Permissions.PrintDocument;

            // Apply encryption with the custom security handler
            ICustomSecurityHandler customHandler = new MyCustomSecurityHandler();
            doc.Encrypt(userPassword, ownerPassword, permissions, customHandler);

            // Save the protected PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF encrypted with custom handler and saved to '{outputPath}'.");
    }
}