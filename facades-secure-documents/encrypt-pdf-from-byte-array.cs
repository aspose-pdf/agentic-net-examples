using System;
using System.IO;
using Aspose.Pdf;

namespace PdfEncryptionExample
{
    class Program
    {
        static void Main()
        {
            // Load the source PDF file into a byte array
            string inputPath = "input.pdf";
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            byte[] sourceBytes = File.ReadAllBytes(inputPath);

            // Encrypt the PDF using passwords
            string userPassword = "user123";
            string ownerPassword = "owner123";
            byte[] encryptedBytes = EncryptPdf(sourceBytes, userPassword, ownerPassword);

            // Save the encrypted PDF to a file for verification
            string outputPath = "encrypted.pdf";
            File.WriteAllBytes(outputPath, encryptedBytes);
            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }

        /// <summary>
        /// Loads a PDF from a byte array, encrypts it, and returns the encrypted PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">The original PDF content.</param>
        /// <param name="userPassword">User password for opening the PDF.</param>
        /// <param name="ownerPassword">Owner password for changing permissions.</param>
        /// <returns>Encrypted PDF bytes.</returns>
        public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword, string ownerPassword)
        {
            // Load PDF from the input byte array
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            {
                using (Document document = new Document(inputStream))
                {
                    // Define permissions (example: allow printing and content extraction)
                    Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt using AES 256-bit algorithm
                    document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                    // Save encrypted PDF to an output memory stream
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        document.Save(outputStream);
                        return outputStream.ToArray();
                    }
                }
            }
        }
    }
}
