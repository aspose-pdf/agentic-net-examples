using System;
using System.IO;
using Aspose.Pdf;

namespace PdfEncryptionExample
{
    public static class PdfEncryptionHelper
    {
        /// <summary>
        /// Reads a PDF from <paramref name="inputStream"/>, encrypts it, and writes the secured PDF to <paramref name="outputStream"/>.
        /// </summary>
        /// <param name="inputStream">Stream containing the original PDF (e.g., a network stream).</param>
        /// <param name="outputStream">Stream where the encrypted PDF will be written.</param>
        /// <param name="userPassword">Password required to open the encrypted PDF.</param>
        /// <param name="ownerPassword">Owner password that grants full permissions.</param>
        public static void EncryptPdfStream(Stream inputStream, Stream outputStream, string userPassword, string ownerPassword)
        {
            // Ensure the input and output streams are not null.
            if (inputStream == null) throw new ArgumentNullException(nameof(inputStream));
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
            if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));
            if (ownerPassword == null) throw new ArgumentNullException(nameof(ownerPassword));

            // Load the PDF from the input stream.
            // Document implements IDisposable, so wrap it in a using block (document-disposal-with-using rule).
            using (Document pdfDoc = new Document(inputStream))
            {
                // Define the permissions you want to allow after encryption.
                // Example: allow printing and content extraction.
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using the recommended AESx256 algorithm (encryption-always-use-CryptoAlgorithm rule).
                pdfDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF directly to the output stream.
                pdfDoc.Save(outputStream);
            }
        }
    }

    // Minimal entry point required for a console application.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage (optional). In real scenarios the streams would come from a network source.
            // Here we simply demonstrate that the program compiles.
            // No operation is performed if the required arguments are not supplied.
            if (args.Length == 4)
            {
                string inputPath = args[0];
                string outputPath = args[1];
                string userPwd = args[2];
                string ownerPwd = args[3];

                using (FileStream input = File.OpenRead(inputPath))
                using (FileStream output = File.Create(outputPath))
                {
                    PdfEncryptionHelper.EncryptPdfStream(input, output, userPwd, ownerPwd);
                }
            }
        }
    }
}