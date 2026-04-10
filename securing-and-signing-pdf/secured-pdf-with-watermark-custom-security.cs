using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Security;

// Custom security handler implementing the ICustomSecurityHandler interface.
// The implementation below provides minimal stub logic required for compilation.
class SimpleSecurityHandler : ICustomSecurityHandler
{
    // Required read‑only properties.
    public string Filter => "Standard";
    public int KeyLength => 128;
    public int Revision => 4;
    public string SubFilter => null;
    public int Version => 2;

    // Stub implementations – real encryption logic is omitted for brevity.
    public byte[] CalculateEncryptionKey(string password) => Array.Empty<byte>();

    // Encrypt/Decrypt now return the processed byte array as required by the interface.
    public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
    {
        // No‑op decryption – simply return the original data slice.
        // In a real implementation you would decrypt the specified range.
        return data;
    }

    public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
    {
        // No‑op encryption – simply return the original data slice.
        // In a real implementation you would encrypt the specified range.
        return data;
    }

    // Permissions must be returned as a byte array according to the interface contract.
    public byte[] EncryptPermissions(int permissions) => BitConverter.GetBytes(permissions);

    // Owner key expects (userPassword, ownerPassword) order.
    public byte[] GetOwnerKey(string userPassword, string ownerPassword) => Array.Empty<byte>();
    public byte[] GetUserKey(string userPassword) => Array.Empty<byte>();

    public void Initialize(EncryptionParameters parameters) { /* No‑op */ }
    public bool IsOwnerPassword(string password) => false;
    public bool IsUserPassword(string password) => false;
}

class Program
{
    static void Main()
    {
        const string outputPath = "secured_watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        // Ensure the watermark image exists.
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a single page with some sample text.
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Sample PDF content.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Apply the watermark overlay to every page.
            foreach (Page p in doc.Pages)
            {
                // ImageStamp is the correct way to stamp an image onto a page.
                ImageStamp stamp = new ImageStamp(watermarkImagePath)
                {
                    // Place the stamp behind the page content (Background = false for overlay).
                    Background = false,
                    // Semi‑transparent watermark.
                    Opacity = 0.3f,
                    // Center the watermark on the page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                p.AddStamp(stamp);
            }

            // Encrypt the document using the custom security handler.
            Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent;
            doc.Encrypt(
                userPassword: "user123",
                ownerPassword: "owner123",
                permissions: perms,
                customHandler: new SimpleSecurityHandler());

            // Save the secured, watermarked PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with custom security handler and watermark: {outputPath}");
    }
}
