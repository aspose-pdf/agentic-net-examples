using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Security;

class SimpleCustomSecurityHandler : ICustomSecurityHandler
{
    // Basic handler properties – values are placeholders.
    public string Filter => "Standard";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => "adbe.pkcs7.s5";
    public int Version => 2;

    // Required methods – simple implementations sufficient for compilation.
    public byte[] CalculateEncryptionKey(string password) => new byte[0];

    public byte[] Decrypt(byte[] data, int offset, int length, byte[] key)
    {
        // For demonstration, return the data unchanged.
        return data;
    }

    public byte[] Encrypt(byte[] data, int offset, int length, byte[] key)
    {
        // For demonstration, return the data unchanged.
        return data;
    }

    public byte[] EncryptPermissions(int permissions)
    {
        // Encode the permissions integer as a byte array (little‑endian).
        return BitConverter.GetBytes(permissions);
    }

    public byte[] GetOwnerKey(string userPassword, string ownerPassword) => new byte[0];

    public byte[] GetUserKey(string userPassword) => new byte[0];

    public void Initialize(EncryptionParameters encryptionParameters)
    {
        // No special initialization required for this simple handler.
    }

    public bool IsOwnerPassword(string password) => false;

    public bool IsUserPassword(string password) => false;
}

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";
        const string warningText   = "WARNING: This document is protected. Copy‑paste is disabled.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Disable copy‑paste by omitting the ExtractContent permission.
            Permissions perms = Permissions.PrintDocument; // allow printing only

            // Apply the custom security handler.
            ICustomSecurityHandler customHandler = new SimpleCustomSecurityHandler();
            doc.Encrypt(userPassword, ownerPassword, perms, customHandler);

            // Add a visible warning overlay on each page.
            foreach (Page page in doc.Pages)
            {
                TextFragment warning = new TextFragment(warningText)
                {
                    // Position near the top‑center of the page.
                    Position = new Position(page.PageInfo.Width / 2, page.PageInfo.Height - 50),
                    // Center the text.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Styling.
                    TextState = { FontSize = 12, ForegroundColor = Color.Red, Font = FontRepository.FindFont("Helvetica") }
                };
                page.Paragraphs.Add(warning);
            }

            // Save the secured PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
    }
}