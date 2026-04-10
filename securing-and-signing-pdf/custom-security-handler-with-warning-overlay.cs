using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Security;
using Aspose.Pdf.Text;

namespace CustomSecurityExample
{
    // Minimal custom security handler – implements required members.
    // For demonstration it does not perform real encryption; it only satisfies the API.
    public class SimpleCustomSecurityHandler : ICustomSecurityHandler
    {
        // Required read‑only properties – return placeholder values.
        public string Filter => "Standard";
        public int KeyLength => 128;
        public int Revision => 4;
        public string SubFilter => "adbe.pkcs7.s5";
        public int Version => 2;

        // Called during encryption to initialise the handler.
        public void Initialize(EncryptionParameters parameters)
        {
            // No special initialisation required for this simple example.
        }

        // Calculate the encryption key – not used in this stub.
        public byte[] CalculateEncryptionKey(string userPassword)
        {
            throw new NotImplementedException();
        }

        // Encrypt a data block – placeholder implementation returns the encrypted bytes.
        public byte[] Encrypt(byte[] data, int offset, int count, byte[] key)
        {
            // Simple stub: return a copy of the requested segment.
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (offset < 0 || count < 0 || offset + count > data.Length)
                throw new ArgumentOutOfRangeException();

            byte[] result = new byte[count];
            Array.Copy(data, offset, result, 0, count);
            return result;
        }

        // Decrypt a data block – placeholder implementation returns the decrypted bytes.
        public byte[] Decrypt(byte[] data, int offset, int count, byte[] key)
        {
            // Simple stub: return a copy of the requested segment.
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (offset < 0 || count < 0 || offset + count > data.Length)
                throw new ArgumentOutOfRangeException();

            byte[] result = new byte[count];
            Array.Copy(data, offset, result, 0, count);
            return result;
        }

        // Encrypt the permissions integer – return the integer as a byte array.
        public byte[] EncryptPermissions(int permissions)
        {
            // Simple placeholder: return the 4‑byte little‑endian representation.
            return BitConverter.GetBytes(permissions);
        }

        // Generate the owner key – not used in this stub.
        public byte[] GetOwnerKey(string userPassword, string ownerPassword)
        {
            throw new NotImplementedException();
        }

        // Generate the user key – not used in this stub.
        public byte[] GetUserKey(string userPassword)
        {
            throw new NotImplementedException();
        }

        // Check if a password is the owner password – simple comparison.
        public bool IsOwnerPassword(string password)
        {
            // In a real handler you would verify against the stored owner key.
            return false;
        }

        // Check if a password is the user password – simple comparison.
        public bool IsUserPassword(string password)
        {
            // In a real handler you would verify against the stored user key.
            return false;
        }
    }

    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "secured_output.pdf";
            const string userPassword = "user123";
            const string ownerPassword = "owner123";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the PDF, add a visible warning overlay, then encrypt with a custom handler.
            using (Document doc = new Document(inputPath))
            {
                // Add a semi‑transparent warning text on each page.
                foreach (Page page in doc.Pages)
                {
                    // Define the rectangle for the annotation (bottom‑left corner).
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 500, 550);

                    // Create a TextAnnotation that will serve as the warning overlay.
                    TextAnnotation warning = new TextAnnotation(page, rect)
                    {
                        Title = "Security Warning",
                        Contents = "This document is protected – copying is disabled.",
                        Color = Aspose.Pdf.Color.Yellow,
                        Opacity = 0.5f,               // Semi‑transparent background.
                        Open = true,
                        Icon = TextIcon.Note
                    };

                    // Add the annotation to the page.
                    page.Annotations.Add(warning);
                }

                // Define permissions that exclude ExtractContent (copy‑paste).
                Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent | Permissions.ModifyTextAnnotations;

                // Apply encryption using the custom security handler.
                ICustomSecurityHandler customHandler = new SimpleCustomSecurityHandler();
                doc.Encrypt(userPassword, ownerPassword, perms, customHandler);

                // Save the encrypted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved with custom security to '{outputPath}'.");
        }
    }
}
