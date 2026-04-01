using System;
using System.IO;
using Aspose.Pdf;

namespace EncryptPdfFromByteArrayExample
{
    class Program
    {
        static void Main()
        {
            // Load an example PDF file into a byte array
            const string inputPath = "input.pdf";
            const string encryptedPath = "encrypted.pdf";
            const string userPassword = "user123";
            const string ownerPassword = "owner123";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            byte[] inputBytes = File.ReadAllBytes(inputPath);
            byte[] encryptedBytes = EncryptPdf(inputBytes, userPassword, ownerPassword);

            // Write the encrypted bytes to a file for verification
            File.WriteAllBytes(encryptedPath, encryptedBytes);
            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'. Size: {encryptedBytes.Length} bytes.");
        }

        /// <summary>
        /// Encrypts a PDF supplied as a byte array and returns the encrypted PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">The original PDF content.</param>
        /// <param name="userPwd">User password.</param>
        /// <param name="ownerPwd">Owner password.</param>
        /// <returns>Encrypted PDF bytes.</returns>
        public static byte[] EncryptPdf(byte[] pdfBytes, string userPwd, string ownerPwd)
        {
            // Load the PDF from the byte array using a MemoryStream
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            using (Document document = new Document(inputStream))
            {
                // Define permissions (example: allow printing and content extraction)
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document with AES-256 encryption
                document.Encrypt(userPwd, ownerPwd, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted document to another MemoryStream
                using (MemoryStream outputStream = new MemoryStream())
                {
                    document.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }
}
