using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Security; // Only core Aspose.Pdf namespaces are used.

class MyCustomSecurityHandler : ICustomSecurityHandler
{
    // Required read‑only properties – simple placeholder values.
    public string Filter => "Standard";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => "adbe.pkcs7.s5";
    public int Version => 2;

    // Called during encryption to initialise the handler.
    public void Initialize(EncryptionParameters parameters) { /* No custom initialisation needed */ }

    // Calculate the encryption key – not used in this simple handler.
    public byte[] CalculateEncryptionKey(string userPassword) => new byte[0];

    // Encrypt data – pass through unchanged (no actual encryption performed here).
    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key) => data;

    // Decrypt data – pass through unchanged (must return a byte[] to satisfy the interface).
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key) => data;

    // Encrypt the permissions field – simple pass‑through of the integer value.
    public byte[] EncryptPermissions(int permissions) => BitConverter.GetBytes(permissions);

    // Owner/User key generation – return empty arrays (no password‑based keys).
    public byte[] GetOwnerKey(string userPassword, string ownerPassword) => new byte[0];
    public byte[] GetUserKey(string userPassword) => new byte[0];

    // Password checks – always return false (no owner/user password validation).
    public bool IsOwnerPassword(string password) => false;
    public bool IsUserPassword(string password) => false;
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, apply a custom security handler, add a visible warning, and save.
        using (Document doc = new Document(inputPath))
        {
            // Disable copy‑paste by omitting the ExtractContent permission.
            Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent | Permissions.ModifyTextAnnotations;

            // Apply encryption with a custom handler (no actual encryption, but handler is attached).
            doc.Encrypt(
                userPassword:   "user123",
                ownerPassword:  "owner123",
                permissions:    perms,
                customHandler:  new MyCustomSecurityHandler()
            );

            // Add a visible warning overlay to each page.
            foreach (Page page in doc.Pages)
            {
                // Full‑page rectangle (coordinates are in points; 0,0 is bottom‑left).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

                TextAnnotation warning = new TextAnnotation(page, rect)
                {
                    Title    = "Security Notice",
                    Contents = "Copying and extracting content is disabled for this document.",
                    Color    = Aspose.Pdf.Color.Red, // Border color
                    Open     = true
                };

                // Ensure the annotation prints with the page.
                warning.Flags = AnnotationFlags.Print;
                page.Annotations.Add(warning);
            }

            // Save the protected PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with custom security handler to '{outputPath}'.");
    }
}
