using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfEncryptionDemo
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "encrypted.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            using (FileStream inputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                Stream encryptedStream = EncryptPdfStream(inputFileStream);
                using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    encryptedStream.CopyTo(outputFileStream);
                }
                encryptedStream.Dispose();
                Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
            }
        }

        /// <summary>
        /// Encrypts the PDF data from the supplied stream using RC4‑40 encryption and returns a stream containing the encrypted PDF.
        /// </summary>
        /// <param name="inputPdfStream">Stream containing the original PDF.</param>
        /// <returns>Stream with the encrypted PDF.</returns>
        public static Stream EncryptPdfStream(Stream inputPdfStream)
        {
            MemoryStream outputStream = new MemoryStream();
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity())
            {
                // Bind the source PDF stream to the facade.
                fileSecurity.BindPdf(inputPdfStream);

                // Apply RC4‑40 encryption. Use any privilege you need; here we allow printing.
                fileSecurity.EncryptFile(
                    "userPassword",
                    "ownerPassword",
                    DocumentPrivilege.Print,
                    KeySize.x40,
                    Algorithm.RC4);

                // Save the encrypted PDF to the output stream.
                fileSecurity.Save(outputStream);
            }
            // Reset the position so the caller can read from the beginning.
            outputStream.Position = 0;
            return outputStream;
        }
    }
}
