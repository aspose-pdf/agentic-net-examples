using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Security;
using Aspose.Pdf.Text;

namespace CustomSecurityExample
{
    // Simple custom security handler – implements the required interface.
    // This example provides no real encryption; it only satisfies the API contract.
    // Permissions are enforced via the Permissions argument passed to Encrypt().
    public class SimpleCustomHandler : ICustomSecurityHandler
    {
        // Required read‑only properties – return placeholder values.
        public string Filter   => "Standard";
        public int    KeyLength => 128;
        public int    Revision  => 4;
        public string SubFilter => "adbe.pkcs7.s5";
        public int    Version   => 2;

        // Called during encryption to initialise the handler.
        public void Initialize(EncryptionParameters parameters)
        {
            // No special initialisation required for this simple handler.
        }

        // Generate a user key – return an empty array for this stub implementation.
        public byte[] GetUserKey(string userPassword) => Array.Empty<byte>();

        // Generate an owner key – return an empty array for this stub implementation.
        public byte[] GetOwnerKey(string userPassword, string ownerPassword) => Array.Empty<byte>();

        // Calculate the encryption key – return an empty array for this stub implementation.
        public byte[] CalculateEncryptionKey(string userPassword) => Array.Empty<byte>();

        // Encrypt a data block – this stub returns the data unchanged.
        public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
        {
            // In a real implementation you would encrypt the specified slice.
            // For the stub we simply return the original array unchanged.
            return data;
        }

        // Decrypt a data block – this stub returns the data unchanged.
        public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
        {
            // In a real implementation you would decrypt the specified slice.
            // For the stub we simply return the original array unchanged.
            return data;
        }

        // Encrypt the permissions integer – return the integer as a 4‑byte array.
        public byte[] EncryptPermissions(int permissions) => BitConverter.GetBytes(permissions);

        // Password checks – always return false for this stub.
        public bool IsOwnerPassword(string password) => false;
        public bool IsUserPassword(string password)  => false;
    }

    class Program
    {
        static void Main()
        {
            const string inputPath  = "input.pdf";
            const string outputPath = "secured_output.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the source PDF.
            using (Document doc = new Document(inputPath))
            {
                // Define permissions: allow printing but disable copy‑paste (ExtractContent).
                Permissions perms = Permissions.PrintDocument; // no ExtractContent flag

                // Apply encryption using the custom handler.
                SimpleCustomHandler customHandler = new SimpleCustomHandler();
                doc.Encrypt(
                    userPassword:   "",               // no user password
                    ownerPassword:  "",               // no owner password
                    permissions:    perms,
                    customHandler:  customHandler);

                // Add a visible warning overlay (watermark) on each page using a TextStamp.
                foreach (Page page in doc.Pages)
                {
                    TextStamp stamp = new TextStamp("Copy‑Paste Disabled");
                    stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                    stamp.TextState.FontSize = 48;
                    stamp.TextState.ForegroundColor = Color.Gray;
                    stamp.Opacity = 0.5f; // semi‑transparent
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Center;

                    page.AddStamp(stamp);
                }

                // Save the secured PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document secured and saved to '{outputPath}'.");
        }
    }
}
