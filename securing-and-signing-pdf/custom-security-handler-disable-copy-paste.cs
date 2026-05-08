using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Security; // Added namespace for EncryptionParameters and ICustomSecurityHandler

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a visible warning overlay on each page
            foreach (Page page in doc.Pages)
            {
                // Define the rectangle where the warning will appear
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 500, 750);

                // Create a text annotation with the warning message
                TextAnnotation warning = new TextAnnotation(page, rect)
                {
                    Title = "Security Notice",
                    Contents = "WARNING: Copy‑paste is disabled for this document.",
                    Color = Aspose.Pdf.Color.Yellow,
                    Opacity = 0.5f,
                    Open = true,
                    Icon = TextIcon.Note
                };

                // Add the annotation to the page
                page.Annotations.Add(warning);
            }

            // Define permissions without ExtractContent (copy‑paste)
            Permissions permissions = Permissions.PrintDocument |
                                      Permissions.ModifyContent |
                                      Permissions.ModifyTextAnnotations;

            // Create an instance of the custom security handler
            ICustomSecurityHandler customHandler = new SimpleCustomSecurityHandler();

            // Apply encryption using the custom handler
            doc.Encrypt(userPassword, ownerPassword, permissions, customHandler);

            // Save the protected PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Protected PDF saved to '{outputPath}'.");
    }

    // Minimal custom security handler implementation
    private class SimpleCustomSecurityHandler : ICustomSecurityHandler
    {
        public string Filter => "Standard";
        public int KeyLength => 128;
        public int Revision => 4;
        public string SubFilter => null;
        public int Version => 2;

        public void Initialize(EncryptionParameters parameters) { /* No custom initialization needed */ }

        public byte[] CalculateEncryptionKey(string userPassword) => new byte[0];

        public byte[] Decrypt(byte[] data, int offset, int count, byte[] key) => data;

        public byte[] Encrypt(byte[] data, int offset, int count, byte[] key) => data;

        public byte[] EncryptPermissions(int permissions) => BitConverter.GetBytes(permissions);

        public byte[] GetOwnerKey(string userPassword, string ownerPassword) => new byte[0];

        public byte[] GetUserKey(string userPassword) => new byte[0];

        public bool IsOwnerPassword(string password) => false;

        public bool IsUserPassword(string password) => false;
    }
}
