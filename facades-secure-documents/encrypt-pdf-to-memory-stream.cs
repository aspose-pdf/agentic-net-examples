using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Encrypt the PDF using PdfFileSecurity facade and write the result to a memory stream.
        using (MemoryStream encryptedStream = new MemoryStream())
        {
            // Initialize the facade and bind the source PDF.
            PdfFileSecurity security = new PdfFileSecurity();
            security.BindPdf(inputPath);

            // Encrypt with full privileges and a 256‑bit key.
            security.EncryptFile(
                userPassword,
                ownerPassword,
                Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                Aspose.Pdf.Facades.KeySize.x256);

            // Save the encrypted PDF into the memory stream.
            security.Save(encryptedStream);

            // Reset the stream position so it can be read/transmitted.
            encryptedStream.Position = 0;

            // Example usage: output the size of the encrypted PDF.
            Console.WriteLine($"Encrypted PDF size: {encryptedStream.Length} bytes");
        }
    }
}