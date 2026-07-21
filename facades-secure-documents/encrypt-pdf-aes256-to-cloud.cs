using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfEncryption
{
    /// <summary>
    /// Provides PDF encryption functionality using AES‑256.
    /// </summary>
    public static class PdfEncryptionUtility
    {
        /// <summary>
        /// Encrypts a PDF file using AES‑256 and writes the encrypted PDF to a cloud storage stream.
        /// </summary>
        /// <param name="inputPdfPath">Full path to the source PDF file.</param>
        /// <param name="outputStream">Stream representing the destination in the cloud storage bucket.</param>
        /// <param name="userPassword">User password (can be null or empty).</param>
        /// <param name="ownerPassword">Owner password (can be null or empty).</param>
        public static void EncryptPdfToCloud(string inputPdfPath, Stream outputStream, string userPassword, string ownerPassword)
        {
            if (string.IsNullOrEmpty(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
            if (outputStream == null)
                throw new ArgumentNullException(nameof(outputStream));

            // PdfFileSecurity implements IDisposable via SaveableFacade, so use a using block.
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                // Bind the source PDF file to the facade.
                fileSecurity.BindPdf(inputPdfPath);

                // Encrypt using AES‑256 (KeySize.x256) and the AES algorithm.
                // DocumentPrivilege.Print is used as an example; adjust as needed.
                fileSecurity.EncryptFile(
                    userPassword,
                    ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256,
                    Algorithm.AES);

                // Save the encrypted PDF directly to the provided cloud stream.
                fileSecurity.Save(outputStream);
            }
        }
    }

    /// <summary>
    /// Minimal entry point required for a console‑type project.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // The project is primarily a library; the Main method is kept empty to satisfy the compiler.
            // Uncomment and adapt the following block to perform a quick test.
            //
            // if (args.Length < 2)
            // {
            //     Console.WriteLine("Usage: PdfEncryption <inputPdfPath> <outputPath>");
            //     return;
            // }
            //
            // string inputPath = args[0];
            // string outputPath = args[1];
            // using (var outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            // {
            //     PdfEncryptionUtility.EncryptPdfToCloud(inputPath, outStream, "userPwd", "ownerPwd");
            // }
            // Console.WriteLine("Encryption completed.");
        }
    }
}
