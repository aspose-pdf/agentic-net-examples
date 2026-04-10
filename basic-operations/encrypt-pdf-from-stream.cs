using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Obtain the input stream that contains the PDF (e.g., from a network source)
        Stream inputStream = GetNetworkInputStream();

        // Obtain the output stream where the encrypted PDF will be written back
        Stream outputStream = GetNetworkOutputStream();

        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Load the PDF from the input stream
        using (Document doc = new Document(inputStream))
        {
            // Define the permissions you want to allow on the encrypted PDF
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt the document using AES‑256
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF to the output stream
            doc.Save(outputStream);
        }

        // Reset the output stream position if the caller expects to read from the beginning
        if (outputStream.CanSeek)
        {
            outputStream.Position = 0;
        }
    }

    // Placeholder: replace with actual logic to obtain the incoming network stream
    static Stream GetNetworkInputStream()
    {
        // Example using a local file; in production this would be a network stream
        return new FileStream("input.pdf", FileMode.Open, FileAccess.Read);
    }

    // Placeholder: replace with actual logic to obtain the outgoing network stream
    static Stream GetNetworkOutputStream()
    {
        // Example using a local file; in production this would be a network stream
        return new FileStream("encrypted_output.pdf", FileMode.Create, FileAccess.Write);
    }
}