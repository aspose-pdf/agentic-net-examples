using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for Permissions and CryptoAlgorithm enums

namespace MyPdfUtilities
{
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Reads a PDF from <paramref name="inputStream"/>, encrypts it with the specified passwords,
        /// and writes the encrypted PDF to <paramref name="outputStream"/>.
        /// </summary>
        /// <param name="inputStream">Stream containing the source PDF (e.g., a network stream).</param>
        /// <param name="outputStream">Stream where the encrypted PDF will be written.</param>
        /// <param name="userPassword">Password required to open the PDF.</param>
        /// <param name="ownerPassword">Owner password that grants full permissions.</param>
        public static void EncryptPdf(
            Stream inputStream,
            Stream outputStream,
            string userPassword,
            string ownerPassword)
        {
            if (inputStream == null) throw new ArgumentNullException(nameof(inputStream));
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
            if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));
            if (ownerPassword == null) throw new ArgumentNullException(nameof(ownerPassword));

            // Load the PDF from the input stream.
            using (Document doc = new Document(inputStream))
            {
                // Define the permissions that will be allowed after encryption.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply encryption using the recommended AES-256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF directly to the output stream.
                doc.Save(outputStream);
            }

            // Ensure the output stream is positioned at the beginning for downstream consumers.
            if (outputStream.CanSeek)
            {
                outputStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – the library functionality is exposed via PdfEncryptionHelper.
        }
    }
}