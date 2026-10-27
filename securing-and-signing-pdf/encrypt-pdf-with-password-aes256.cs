using System;
using System.IO;
using Aspose.Pdf;

namespace PdfEncryptionDemo
{
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Loads a PDF from a byte array, encrypts it with the specified user password,
        /// and returns the encrypted PDF as a byte array.
        /// </summary>
        /// <param name="pdfBytes">Original PDF content.</param>
        /// <param name="userPassword">Password required to open the encrypted PDF.</param>
        /// <returns>Encrypted PDF bytes.</returns>
        public static byte[] EncryptPdf(byte[] pdfBytes, string userPassword)
        {
            if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));
            if (string.IsNullOrEmpty(userPassword)) throw new ArgumentException("User password must be provided.", nameof(userPassword));

            // Load the PDF from the input byte array.
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            using (Document document = new Document(inputStream))
            {
                // Define permissions (example: allow printing and content extraction).
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

                // Owner password can be empty or a separate strong password.
                string ownerPassword = "";

                // Encrypt using AES‑256 (recommended).
                document.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted document to a memory stream and return its bytes.
                using (MemoryStream outputStream = new MemoryStream())
                {
                    document.Save(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Placeholder entry point to satisfy the compiler.
            // Real usage would call PdfEncryptionHelper.EncryptPdf with appropriate arguments.
        }
    }
}